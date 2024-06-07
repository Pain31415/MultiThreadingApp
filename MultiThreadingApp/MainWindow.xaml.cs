using System;
using System.Threading;
using System.Windows;

namespace MultiThreadingApp
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void StartButton_Click(object sender, RoutedEventArgs e)
        {
            if (int.TryParse(startTextBox.Text, out int startRange) &&
                int.TryParse(endTextBox.Text, out int endRange) &&
                int.TryParse(threadTextBox.Text, out int threadCount))
            {
                int rangePerThread = (endRange - startRange + 1) / threadCount;

                Thread[] threads = new Thread[threadCount];

                for (int i = 0; i < threadCount; i++)
                {
                    int start = startRange + i * rangePerThread;
                    int end = (i == threadCount - 1) ? endRange : start + rangePerThread - 1;

                    Thread thread = new Thread(() => PrintNumbers(start, end));
                    thread.Start();
                    threads[i] = thread;
                }

                foreach (var thread in threads)
                {
                    thread.Join();
                }

                MessageBox.Show("All threads completed.");
            }
            else
            {
                MessageBox.Show("Invalid input. Please enter valid integers.");
            }
        }

        private void PrintNumbers(int start, int end)
        {
            for (int i = start; i <= end; i++)
            {
                Dispatcher.Invoke(() =>
                {
                    resultTextBox.AppendText(i.ToString() + Environment.NewLine);
                });
                Thread.Sleep(100);
            }
        }
    }
}