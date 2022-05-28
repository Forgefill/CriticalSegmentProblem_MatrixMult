using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Threading;
namespace MatrixMult
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        List<List<int>> matA = new List<List<int>>();
        List<List<int>> matB = new List<List<int>>();
        public MainWindow()
        {
            InitializeComponent();

            A.ShowGridLines = true;
            B.ShowGridLines = true;
            C.ShowGridLines= true;
            //ColumnDefinition colDef1 = new ColumnDefinition();
            //ColumnDefinition colDef2 = new ColumnDefinition();
            //ColumnDefinition colDef3 = new ColumnDefinition();
            //A.ColumnDefinitions.Add(colDef1);
            //A.ColumnDefinitions.Add(colDef2);
            //A.ColumnDefinitions.Add(colDef3);

            //RowDefinition rowDef1 = new RowDefinition();
            //RowDefinition rowDef2 = new RowDefinition();
            //RowDefinition rowDef3 = new RowDefinition();
            //A.RowDefinitions.Add(rowDef1);
            //A.RowDefinitions.Add(rowDef2);
            //A.RowDefinitions.Add(rowDef3);

            //ColumnDefinition BcolDef1 = new ColumnDefinition();
            //ColumnDefinition BcolDef2 = new ColumnDefinition();
            //ColumnDefinition BcolDef3 = new ColumnDefinition();
            //B.ColumnDefinitions.Add(BcolDef1);
            //B.ColumnDefinitions.Add(BcolDef2);
            //B.ColumnDefinitions.Add(BcolDef3);

            //RowDefinition BrowDef1 = new RowDefinition();
            //RowDefinition BrowDef2 = new RowDefinition();
            //RowDefinition BrowDef3 = new RowDefinition();
            //B.RowDefinitions.Add(BrowDef1);
            //B.RowDefinitions.Add(BrowDef2);
            //B.RowDefinitions.Add(BrowDef3);

            matrixInit(3, 3, 3);
        }

        private void matrixInit(int N ,int M, int K)
        {
            matA.Clear();
            matB.Clear();
            Random rand = new Random();

            for (int i = 0; i < M; i++)
            {
                A.ColumnDefinitions.Add(new ColumnDefinition());
                B.RowDefinitions.Add(new RowDefinition());
                
            }
            for(int i = 0; i < N; i++)
            {
                A.RowDefinitions.Add(new RowDefinition());
                C.RowDefinitions.Add(new RowDefinition());
            }

            for( int i = 0; i < K; i++)
            {
                B.ColumnDefinitions.Add(new ColumnDefinition());
                C.ColumnDefinitions.Add(new ColumnDefinition());
            }

            for(int i = 0; i < N; i++)
            {
                matA.Add(new List<int>());

                for(int j = 0; j < M; j++)
                {
                    int valA = rand.Next(0, 100);
                    matA[i].Add(valA);
                    
                    TextBlock txtBlock1 = new TextBlock();
                    txtBlock1.Text = valA.ToString();
                    txtBlock1.FontSize = 14;
                    txtBlock1.FontWeight = FontWeights.Bold;
                    txtBlock1.Foreground = new SolidColorBrush(Colors.Green);
                    txtBlock1.VerticalAlignment = VerticalAlignment.Center;
                    txtBlock1.HorizontalAlignment = HorizontalAlignment.Center;
                    Grid.SetRow(txtBlock1, i);
                    Grid.SetColumn(txtBlock1, j);

                    A.Children.Add(txtBlock1);
                }
            }

            for (int i = 0; i < M; i++)
            {
                matB.Add(new List<int>());

                for (int j = 0; j < K; j++)
                {
                    int valA = rand.Next(0, 10);
                    matB[i].Add(valA);

                    TextBlock txtBlock1 = new TextBlock();
                    txtBlock1.Text = valA.ToString();
                    txtBlock1.FontSize = 14;
                    txtBlock1.FontWeight = FontWeights.Bold;
                    txtBlock1.Foreground = new SolidColorBrush(Colors.Green);
                    txtBlock1.VerticalAlignment = VerticalAlignment.Center;
                    txtBlock1.HorizontalAlignment = HorizontalAlignment.Center;
                    Grid.SetRow(txtBlock1, i);
                    Grid.SetColumn(txtBlock1, j);

                    B.Children.Add(txtBlock1);
                }
            }
        }
        private void TextBox_TextInput(object sender, TextCompositionEventArgs e)
        {
            
        }

        private void SizeSet_Click(object sender, RoutedEventArgs e)
        {
            int n, m, k;
            if(int.TryParse(N.Text,out n) && int.TryParse(M.Text,out m) &&int.TryParse(K.Text,out k))
            {
                A.ColumnDefinitions.Clear();
                A.RowDefinitions.Clear();
                B.ColumnDefinitions.Clear();
                B.RowDefinitions.Clear();
                C.RowDefinitions.Clear();
                C.ColumnDefinitions.Clear();
                A.Children.Clear();
                B.Children.Clear();
                C.Children.Clear();
                matrixInit(n, m, k);
            }
        }

        private  async Task<List<int>> multVM(List<int> v, List<List<int>> mat, int row)
        {
            return await Task.Run( () =>
            {
                Random a = new Random();

                Thread.Sleep(a.Next(1000, 10000));

                List<int> res = new List<int>();
                for (int i = 0; i < mat[0].Count; i++)
                {
                    int sum = 0;
                    for (int j = 0; j < v.Count; j++)
                    {
                        sum += v[j] * mat[j][i];
                    }

                    res.Add(sum);
                }
                res.Add(row);
                return res;
            }
            );
        }

        private void setRes(List<int> r, int row)
        {
            r.RemoveAt(r.Count - 1);
            for (int j = 0; j < matB[0].Count; j++)
            {
                    TextBlock txtBlock1 = new TextBlock();
                    txtBlock1.Text = r[j].ToString();
                    txtBlock1.FontSize = 14;
                    txtBlock1.FontWeight = FontWeights.Bold;
                    txtBlock1.Foreground = new SolidColorBrush(Colors.Green);
                    txtBlock1.VerticalAlignment = VerticalAlignment.Center;
                    txtBlock1.HorizontalAlignment = HorizontalAlignment.Center;
                    Grid.SetRow(txtBlock1, row);
                    Grid.SetColumn(txtBlock1, j);

                    C.Children.Add(txtBlock1);
            }
        }
        private void Mult_Click(object sender, RoutedEventArgs e)
        {
            var scheduler = TaskScheduler.FromCurrentSynchronizationContext();

            int m = matA.Count;
            Task[] tasks = new Task[matA.Count];
            for (int i = 0; i < matA.Count; i++)
            {
                tasks[i] = multVM(matA[i], matB, i).ContinueWith((t) => setRes(t.Result, t.Result.Last()), scheduler);
            }

        }
    }
}
