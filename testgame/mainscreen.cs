using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics.PerformanceData;
using System.Drawing;
using System.Linq;
using System.Reflection.Emit;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using testgame;
using static System.Net.Mime.MediaTypeNames;

namespace testgame
{


    public partial class mainscreen : UserControl
    {
        public int Para;
        public int KullaniciID;
        public int Yas;
        public string ad;
        int[] InventoryID = new int[200];
        string[] item = new string[200];
        string[] newitems = new string[200];
        bool shopadditem = true;
        // public int KullaniciID { get; set; }  // Kullanıcı ID'si değişkeni
        private string connectionString = "Server=DESKTOP-ETBTTTD\\MSSQLSERVER01;Database=oyun_veritabani;Integrated Security=True;";

        public mainscreen()
        {

            InitializeComponent();
            this.listBox1.SelectedIndexChanged += new EventHandler(this.listBox1_SelectedIndexChanged);

        }

        private void mainscreen_Load(object sender, EventArgs e)
        {

            // LoadInventoryData();
        }

        public void LoadInventoryData()
        {

            listBox1.Items.Clear();
            int sayac = 0;
            label5.Text = "Para: ";
            label3.Text = Convert.ToString(Para);
            label4.Text = "Kullanıcı: " + KullaniciID;

            string query = "SELECT InventoryID, ItemName, ItemLevel, ItemRarity, ItemType, HP, STR, DEF, Efsun1, Efsun2,Para FROM Inventory WHERE KullaniciID = @KullaniciID";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@KullaniciID", KullaniciID); // KullaniciID parametre olarak ekleniyor

                    try
                    {
                        connection.Open(); // Bağlantıyı aç
                        int t = 1;
                        while (Yas >= t)
                        {
                            //itemgenerator create = new itemgenerator();
                            //string newitem = create.create(Yas);
                            //string[] words = newitem.Split("*");
                            //int s = 0;

                            //foreach (var word in words)
                            //{
                            //    newitems[s] = (word.Trim());

                            //    s++;

                            //}
                           
                            if (shopadditem)
                            {
                                shop_add_item();
                                shopadditem = false;
                            }
                            t++;
                            //listBox2.Items.Add(newitems[1]);
                        }
                        int inventory_sayac = 0;
                        // Sorguyu çalıştır ve verileri oku
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read()) // Sadece ilk kaydı okur
                            {
                                // StringBuilder kullanarak detayları oluştur
                                StringBuilder inventoryDetails = new StringBuilder();
                                inventoryDetails.AppendLine($"Item Name: {reader["ItemName"]},\n ");
                                StringBuilder itemDetails = new StringBuilder();
                                InventoryID[inventory_sayac] = Convert.ToInt32(reader["InventoryID"]);
                                inventory_sayac++;
                                itemDetails.AppendLine($"Item Name: {reader["ItemName"]},\n " +
                                                             $"Item Level: {reader["ItemLevel"]},\n " +
                                                             $"Rarity: {reader["ItemRarity"]},\n " +
                                                             $"Type: {reader["ItemType"]},\n " +
                                                             $"HP: {reader["HP"]},\n " +
                                                             $"STR: {reader["STR"]},\n " +
                                                             $"DEF: {reader["DEF"]},\n " +
                                                             $"Efsun1: {reader["Efsun1"]},\n " +
                                                             $"Efsun2: {reader["Efsun2"]} \n" +
                                                             $"Para: {reader["Para"]}"
                                                             );

                                item[sayac] = itemDetails.ToString();
                                sayac++;


                                listBox1.Items.Add(inventoryDetails);


                            }


                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Bir hata oluştu: " + ex.Message);
                    }

                }
            }
        }

        private void mainscreen_Click(object sender, EventArgs e)
        {

            //  LoadInventoryData();
        }

        private void mainscreen_Enter(object sender, EventArgs e)
        {
            //  LoadInventoryData();
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            int selectedIndex = listBox1.SelectedIndex;
            if (selectedIndex < 0)
            {
                return;
            }
            else
            {
                label1.Text = item[selectedIndex];
            }


        }
        private void shop_reload()
        {
           
            int selectedIndex = listBox2.SelectedIndex;
            int düzeltici = selectedIndex;
            if (selectedIndex < 0)
            {
                return;
            }
            else
            {
                string shopitem;

              
                     shopitem = "İtem Name: " + newitems[selectedIndex * 10 + 1+düzeltici] + "\n" 
                    + "İtem Level: " + newitems[selectedIndex * 10 + 2 + düzeltici] + "\n" 
                    + "İtem Rarity: " + newitems[selectedIndex * 10 + 3 + düzeltici] + "\n" 
                    + "İtem Type: " + newitems[selectedIndex * 10 + 4 + düzeltici] + "\n" 
                    + "HP: " + newitems[selectedIndex * 10 + 5 + düzeltici] + "\n" 
                    + "STR: " + newitems[selectedIndex * 10 + 6 + düzeltici] + "\n" 
                    + "DEF: " + newitems[selectedIndex * 10 + 7 + düzeltici] + "\n" 
                    + "Enchant: " + newitems[selectedIndex * 10 + 8 + düzeltici] + "\n" 
                    + "Enchant: " + newitems[selectedIndex * 10 + 9 + düzeltici] + "\n" 
                    + "Para: " + newitems[selectedIndex * 10 + 10 + düzeltici];

                
              
                       
                
                label2.Text = shopitem;
            }
        }
        private void listBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            shop_reload();
          

        }

        public void shop_add_item()
        {



            string query = "SELECT InventoryID, ItemName, ItemLevel, ItemRarity, ItemType, HP, STR, DEF, Efsun1, Efsun2,Para FROM Inventory WHERE KullaniciID = @KullaniciID";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@KullaniciID", KullaniciID); // KullaniciID parametre olarak ekleniyor


                    connection.Open(); // Bağlantıyı aç
                    int t = 1;
                    int r = 0;
                    while (Yas >= t)
                    {

                        itemgenerator create = new itemgenerator();
                        string newitem = create.create(Yas);
                        string[] words = newitem.Split("*");
                        int s = 0;


                        while (!string.IsNullOrEmpty(newitems[s]))
                        {
                            s++;
                        }
                        int d = 0;
                     
                        foreach (var word in words)
                        {

                            newitems[s] = (word.Trim());






                            s++;

                        }
                        t++;                    
                        while (listBox2.Items.Count != r)
                        {
                           r++;
                        }
                        
                        
                        listBox2.Items.Add(newitems[s/10-r]);
                    }
                }
            }
        }

        public void buy_item()
        {
            int selectedIndex = listBox2.SelectedIndex;

            // buyitem stringini oluştur

            string buyitem = KullaniciID + "*" + newitems[selectedIndex * 10 + 1] + "*" +
                             newitems[selectedIndex * 10 + 2] + "*" + newitems[selectedIndex * 10 + 3] + "*" +
                             newitems[selectedIndex * 10 + 4] + "*" + newitems[selectedIndex * 10 + 5] + "*" +
                             newitems[selectedIndex * 10 + 6] + "*" + newitems[selectedIndex * 10 + 7] + "*" +
                             newitems[selectedIndex * 10 + 8] + "*" + newitems[selectedIndex * 10 + 9] + "*" +
                             newitems[selectedIndex * 10 + 10];

            // buyitem stringini '*' ile böl
            string[] itemData = buyitem.Split('*');

            // Veritabanına kaydetme işlemi
            string insertQuery = "INSERT INTO Inventory (KullaniciID, ItemName, ItemLevel, ItemRarity, ItemType, HP, Str, Def, Efsun1, Efsun2,Para) " +
                                 "VALUES (@KullaniciID, @ItemName, @ItemLevel, @ItemRarity, @ItemType, @HP, @Str, @Def, @Efsun1, @Efsun2,@Para)";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(insertQuery, connection))
                {
                    // Parametreleri ekle
                    command.Parameters.AddWithValue("@KullaniciID", itemData[0]);
                    command.Parameters.AddWithValue("@ItemName", itemData[1]);
                    command.Parameters.AddWithValue("@ItemLevel", itemData[2]);
                    command.Parameters.AddWithValue("@ItemRarity", itemData[3]);
                    command.Parameters.AddWithValue("@ItemType", itemData[4]);
                    command.Parameters.AddWithValue("@HP", itemData[5]);
                    command.Parameters.AddWithValue("@Str", itemData[6]);
                    command.Parameters.AddWithValue("@Def", itemData[7]);
                    command.Parameters.AddWithValue("@Efsun1", itemData[8]);
                    command.Parameters.AddWithValue("@Efsun2", itemData[9]);
                    command.Parameters.AddWithValue("@Para", itemData[10]);
                    try
                    {
                        connection.Open(); // Bağlantıyı aç
                        int result = command.ExecuteNonQuery(); // Sorguyu çalıştır

                        if (result > 0)
                        {

                            MessageBox.Show("İtem başarıyla satın alındı ve veritabanına kaydedildi.");

                            LoadInventoryData();
                            listBox2.Items.Clear();
                            label2.Text = "Satın Alındı!";
                        }
                        else
                        {
                            MessageBox.Show("İtem kaydedilemedi, lütfen tekrar deneyin.");
                            LoadInventoryData();
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Bir hata oluştu: " + ex.Message);
                    }
                }
            }
        }

        public void givemoney()
        {
            string query = "UPDATE Kullanici SET Para = Para + @Miktar WHERE ID = @KullaniciID";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    // @Miktar ve @KullaniciID parametrelerini ekliyoruz
                    command.Parameters.AddWithValue("@Miktar", 10);
                    command.Parameters.AddWithValue("@KullaniciID", KullaniciID);

                    // Komutu çalıştırıyoruz
                    int rowsAffected = command.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        Para = Para + 10;
                        LoadInventoryData();
                    }
                    else
                    {
                        MessageBox.Show("Güncelleme sırasında bir hata oluştu.");
                    }
                }
            }
        }
        public void removeitem()
        {
            int selectedIndex = listBox1.SelectedIndex;
            if (selectedIndex < 0)
            {

            }


            else
            {


                string query = "DELETE FROM Inventory WHERE KullaniciID = @KullaniciID AND InventoryID = @InventoryID";

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {

                        command.Parameters.AddWithValue("@KullaniciID", KullaniciID);
                        command.Parameters.AddWithValue("@InventoryID", InventoryID[selectedIndex]);
                        command.ExecuteNonQuery();
                        LoadInventoryData();
                        label1.Text = ("");
                        MessageBox.Show("Eşya Kaldırıldı");


                    }
                }
            }
        }


        private void button1_Click(object sender, EventArgs e)
        {
            int selectedIndex = listBox2.SelectedIndex;
            int düzeltici = selectedIndex;
            
            if (selectedIndex < 0)
            {

            }
            else
            {
                string kmoney = label3.Text;
                if (Convert.ToInt32(kmoney) >= Convert.ToInt32(newitems[selectedIndex * 10 + 10 + düzeltici]))
                {
                    int newmoney = Convert.ToInt32(kmoney) - Convert.ToInt32(newitems[selectedIndex * 10 + 10 + düzeltici]);
                    string query = "UPDATE Kullanici SET Para = @Para WHERE ID = @KullaniciID";

                    using (SqlConnection connection = new SqlConnection(connectionString))
                    {
                        connection.Open(); // Bağlantıyı açıyoruz

                        using (SqlCommand command = new SqlCommand(query, connection))
                        {
                            // @Para ve @KullaniciID parametrelerini ekliyoruz
                            command.Parameters.AddWithValue("@Para", newmoney);
                            command.Parameters.AddWithValue("@KullaniciID", KullaniciID); // KullaniciID'yi daha önce tanımlamış olmalısınız

                            // Komutu çalıştırıyoruz
                            int rowsAffected = command.ExecuteNonQuery(); // Güncellenen satır sayısını döndürür
                            if (rowsAffected > 0)
                            {
                                // Güncelleme başarılı
                                Para = newmoney;
                            }
                            else
                            {
                                // Güncelleme başarısız
                                MessageBox.Show("Güncelleme sırasında bir hata oluştu.");
                            }
                        }
                    }









                    buy_item();


                }



            }


        }

        private void button2_Click(object sender, EventArgs e)
        {
            removeitem();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            givemoney();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            shopadditem = true;
            shop_add_item();
        }
    }
}
