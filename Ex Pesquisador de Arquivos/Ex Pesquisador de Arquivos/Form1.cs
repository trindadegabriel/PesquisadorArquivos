namespace Ex_Pesquisador_de_Arquivos
{
    public partial class Form1 : Form
    {
        
        public Form1()
        {
            InitializeComponent();
        }
        

        private void BtnPesquisar(object sender, EventArgs e)
        {
            if(textBox1.Text == "")
            {
                MessageBox.Show("Por favor insira um conteúdo para iniciar a pesquisa");
            }
            else
            {

                var resultados = pesquisaArquivos("D:\\", textBox1.Text);
                foreach(var resultado in resultados)
                {
                    listBox1.Items.Add(resultado);
                }

               if(listBox1.Items.Count < 1)
                {
                    MessageBox.Show("Não foi encontrado nenhum arquivo com esse nome, digite novamente.");
                }
                    
            }
        }


        private List<string> pesquisaArquivos(string diretorio, string pesquisa)
        {
            List<string> lista = new List<string>();
            string[] files = Directory.GetFiles(diretorio, "*");
            string[] directories = Directory.GetDirectories(diretorio);

            foreach (string dir in directories)
            {
                try
                {
                    List<string> recebe;
                    recebe = pesquisaArquivos(dir, pesquisa);
                    lista.AddRange(recebe);
                }
                catch (Exception ex) { }
            }

            foreach (string f in files)
            {
                try
                {
                    string fileName = Path.GetFileName(f);
                    if (fileName.Contains(pesquisa))
                        lista.Add(fileName);
                }
                catch (Exception ex) { }
            }
            return lista;
        }


        private void btnLimpar(object sender, EventArgs e)
        {
            listBox1.Items.Clear();
            textBox1.Text = ("");
        }


        private void btnSalvar(object sender, EventArgs e)
        { 
            if (listBox1.Items.Count >= 1)
            {
                string caminhoarquivo = @"D:\Gabriel\logpesquisa.txt";
                File.WriteAllLines(caminhoarquivo, listBox1.Items.Cast<string>());
                MessageBox.Show("Arquivo salvo!");
            }
            else
                MessageBox.Show("Não existe nenhum arquivo para ser salvo");
        }
    }
}