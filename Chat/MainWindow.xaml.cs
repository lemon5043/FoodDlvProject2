using Microsoft.AspNetCore.SignalR.Client;
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


namespace Chat
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        HubConnection connection;
        public MainWindow()
        {
            InitializeComponent();
            connection = new HubConnectionBuilder()
             .WithUrl("https://localhost:7046/chat/")
             .Build();
            connection.On<string>("online", (msg) =>
            {
                this.Dispatcher.Invoke(() =>
                {
                    txtInfo.Text = msg + "\r\n";
                });

            });
            connection.On<string, string>("ReceiveMessage", (user, msg) =>
            {
                this.Dispatcher.Invoke(() =>
                {
                    txtMsg.Text += $"{user}:{msg} \r\n";
                });
            });
            connection.StartAsync();
        }
        /// <summary>
        /// 加入聊天室
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnIn_Click(object sender, RoutedEventArgs e)
        {
            string title = $"客服{new Random().Next(1, 20)}號";
            Title = title;

            connection.InvokeAsync("Login", title);
            btnSend.IsEnabled = true;
        }
        /// <summary>
        /// 離開聊天室
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnOut_Click(object sender, RoutedEventArgs e)
        {
            connection.InvokeAsync("SignOut", Title);
            connection.StopAsync();
            this.Close();
        }
        private void btnSend_Click(object sender, RoutedEventArgs e)
        {
            if (txtSend.Text == "") return;

            connection.InvokeAsync("SendMessage", Title, txtSend.Text);
        }
    }
    
}
