using IWshRuntimeLibrary;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using File = System.IO.File;

namespace ProjetoAtalho
{
    public partial class Form1 : Form
    {
        string path = "";
        private DirectoryInfo directoryInfoRoot = null;

        public Form1()
        {
            InitializeComponent();
        }

        private void btnMapear_Click(object sender, EventArgs e)
        {
            if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
            {
                directoryInfoRoot = new DirectoryInfo(folderBrowserDialog1.SelectedPath);

                Iteracao(directoryInfoRoot, null);
            }
            treeView.ExpandAll();
        }

        private void Iteracao(DirectoryInfo dic, TreeNode node)
        {
            TreeNode actNode = null;
            if (node == null)
            {
                treeView.Nodes.Clear();
                actNode = treeView.Nodes.Add(dic.Name);
            }else
            {
                actNode = node.Nodes.Add(dic.Name);
            }

            actNode.Tag = dic.FullName;

            if (dic.GetDirectories().Length > 0)
            {
                foreach (var item in dic.GetDirectories())
                {
                    Iteracao(item, actNode);
                }
            }

            foreach (var file in dic.GetFiles())
            {
                var fileNode = actNode.Nodes.Add(file.Name);
                fileNode.Tag = file.FullName;
            }
        }

        private void btnAtalho_Click(object sender, EventArgs e)
        {
            if (treeView.SelectedNode == null)
            {
                MessageBox.Show("Selecione um no primeiramente");
                return;
            }

            openFileDialog1.Multiselect = true;

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {

                foreach (var file in openFileDialog1.FileNames)
                {
                    CreateShortcut(file, treeView.SelectedNode.Tag as string);
                }
                
            }

            btnAtualizar_Click(sender, e);
        }

        private string RemoveCaminho(string path)
        {
            return path.Substring(0, path.LastIndexOf('\\'));
        }
        private string RemoveNome(string path)
        {
            return path.Substring(path.LastIndexOf('\\') + 1, path.Length - path.LastIndexOf('\\') - 1);
        }
        private void CreateShortcut(string originPath, string destinyPath)
        {
            string link = destinyPath + "\\" + RemoveNome(originPath) + ".lnk";
            var shell = new WshShell();
            var shortcut = shell.CreateShortcut(link) as IWshShortcut;
            shortcut.TargetPath = originPath;
            shortcut.WorkingDirectory = destinyPath;
            shortcut.Save();
        }

        private void btnAtualizar_Click(object sender, EventArgs e)
        {
            if (directoryInfoRoot == null)
            {
                if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
                {
                    directoryInfoRoot = new DirectoryInfo(folderBrowserDialog1.SelectedPath);
                }
            }

            Iteracao(directoryInfoRoot, null);

            treeView.ExpandAll();
        }

        private void btnCompilar_Click(object sender, EventArgs e)
        {
            if (directoryInfoRoot == null)
            {
                MessageBox.Show("Não há projeto para compilar");
                return;
            }

            if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
            {
                IteracaoArvore(new DirectoryInfo(treeView.Nodes[0].Tag as string), new DirectoryInfo(folderBrowserDialog1.SelectedPath));
                
            }

            MessageBox.Show("Projeto Compilado");
        }

        private void IteracaoArvore(DirectoryInfo origem, DirectoryInfo destino)
        {
            //Verifica se há Arquivos para serem copiados
            foreach (var file in origem.EnumerateFiles())
            {
                CopiaArquivo(file, destino);
            }

            //Cria as novas pastas
            foreach (var pasta in origem.EnumerateDirectories())
            {
                var path = destino.FullName + "\\" + pasta.Name;
                Directory.CreateDirectory(path);
                IteracaoArvore(pasta, new DirectoryInfo(path));
            }

            
        }

        private void CopiaArquivo(FileInfo file, DirectoryInfo destino)
        {
            string target = file.FullName;
            if (file.Extension.StartsWith(".lnk"))
            {
                target = GetShortcutTarget(file.FullName);
            }

            File.Copy(target, destino.FullName + "\\" + RemoveNome(target), false);
        }

        private string GetShortcutTarget(string file)
        {
            try
            {
                if (System.IO.Path.GetExtension(file).ToLower() != ".lnk")
                {
                    throw new Exception("Supplied file must be a .LNK file");
                }

                FileStream fileStream = File.Open(file, FileMode.Open, FileAccess.Read);
                using (System.IO.BinaryReader fileReader = new BinaryReader(fileStream))
                {
                    fileStream.Seek(0x14, SeekOrigin.Begin);     // Seek to flags
                    uint flags = fileReader.ReadUInt32();        // Read flags
                    if ((flags & 1) == 1)
                    {                      // Bit 1 set means we have to
                                           // skip the shell item ID list
                        fileStream.Seek(0x4c, SeekOrigin.Begin); // Seek to the end of the header
                        uint offset = fileReader.ReadUInt16();   // Read the length of the Shell item ID list
                        fileStream.Seek(offset, SeekOrigin.Current); // Seek past it (to the file locator info)
                    }

                    long fileInfoStartsAt = fileStream.Position; // Store the offset where the file info
                                                                 // structure begins
                    uint totalStructLength = fileReader.ReadUInt32(); // read the length of the whole struct
                    fileStream.Seek(0xc, SeekOrigin.Current); // seek to offset to base pathname
                    uint fileOffset = fileReader.ReadUInt32(); // read offset to base pathname
                                                               // the offset is from the beginning of the file info struct (fileInfoStartsAt)
                    fileStream.Seek((fileInfoStartsAt + fileOffset), SeekOrigin.Begin); // Seek to beginning of
                                                                                        // base pathname (target)
                    long pathLength = (totalStructLength + fileInfoStartsAt) - fileStream.Position - 2; // read
                                                                                                        // the base pathname. I don't need the 2 terminating nulls.
                    char[] linkTarget = fileReader.ReadChars((int)pathLength); // should be unicode safe
                    var link = new string(linkTarget);

                    int begin = link.IndexOf("\0\0");
                    if (begin > -1)
                    {
                        int end = link.IndexOf("\\\\", begin + 2) + 2;
                        end = link.IndexOf('\0', end) + 1;

                        string firstPart = link.Substring(0, begin);
                        string secondPart = link.Substring(end);

                        return firstPart + secondPart;
                    }
                    else
                    {
                        return link;
                    }
                }
            }
            catch
            {
                return "";
            }
        }

        private void btnAbrirPasta_Click(object sender, EventArgs e)
        {
            if (directoryInfoRoot != null)
            {
                OpenAndSelectPath(folderBrowserDialog1.SelectedPath);
            }else
                MessageBox.Show("Selecione uma pasta/projeto");

        }

        public void OpenAndSelectPath(string path)
        {
            bool isfile = System.IO.File.Exists(path);
            if (isfile)
            {
                string argument = @"/select, " + path;
                System.Diagnostics.Process.Start("explorer.exe", argument);
            }
            else
            {
                bool isfolder = System.IO.Directory.Exists(path);
                if (isfolder)
                {
                    string argument = @"/select, " + path;
                    System.Diagnostics.Process.Start("explorer.exe", argument);
                }
            }
        }
    }
}
