using System;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace testgame
{
    public partial class Form1 : Form
    {
        public int KullaniciID { get; set; }
        public int Yas { get; set; }
        public int Para { get; set; }
        public string ad { get; set; }
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string connectionString = "Server=DESKTOP-ETBTTTD\\MSSQLSERVER01;Database=oyun_veritabani;Integrated Security=True;";
             ad = textBox1.Text;
            string soyad = textBox2.Text;

            string checkQuery = "SELECT ID, Yas, Para FROM Kullanici WHERE Ad = @Ad AND Soyad = @Soyad";
            string insertQuery = "INSERT INTO Kullanici (Ad, Soyad, Yas, Para) VALUES (@Ad, @Soyad, @Yas, @Para)";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(checkQuery, connection))
                {
                    command.Parameters.AddWithValue("@Ad", ad);
                    command.Parameters.AddWithValue("@Soyad", soyad);

                    try
                    {
                        connection.Open();
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                KullaniciID = reader.GetInt32(0);
                                Yas = reader.GetInt32(1);
                                Para = reader.GetInt32(2); // Para de�erini al
                               
                                MessageBox.Show($"Giri� ba�ar�l�! Kullan�c� ID: {KullaniciID}, Para: {Para}");
                            
                                ShowMainScreen();
                                return;
                            }
                        }

                        // Kullan�c� bulunamazsa yeni ekleme yap�lacak
                        using (SqlCommand insertCommand = new SqlCommand(insertQuery, connection))
                        {
                            insertCommand.Parameters.AddWithValue("@Ad", ad);
                            insertCommand.Parameters.AddWithValue("@Soyad", soyad);
                            insertCommand.Parameters.AddWithValue("@Yas", 1); // Varsay�lan ya�
                            insertCommand.Parameters.AddWithValue("@Para", 10); // Varsay�lan para miktar�

                            int insertResult = insertCommand.ExecuteNonQuery();

                            if (insertResult > 0)
                            {
                                // Yeni kullan�c� eklendikten sonra tekrar kontrol et
                                command.CommandText = checkQuery;
                                using (SqlDataReader newReader = command.ExecuteReader())
                                {
                                    if (newReader.Read())
                                    {
                                        KullaniciID = newReader.GetInt32(0);
                                        Yas = newReader.GetInt32(1);
                                        Para = newReader.GetInt32(2);

                                        MessageBox.Show($"Yeni kullan�c� eklendi! Kullan�c� ID: {KullaniciID}, Para: {Para}");
                                        ShowMainScreen();
                                    }
                                }
                            }
                            else
                            {
                                MessageBox.Show("Kullan�c� eklenemedi, l�tfen tekrar deneyin.");
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Bir hata olu�tu: " + ex.Message);
                    }
                }
            }
        }

        private void ShowMainScreen()
        {
            mainscreen fr = new mainscreen
            {
                KullaniciID = this.KullaniciID,
                Yas = this.Yas,
                Para = this.Para
            };

            fr.Dock = DockStyle.Fill;
            this.Controls.Clear();
            this.Controls.Add(fr);
            fr.Show();
            fr.LoadInventoryData();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            mainscreen1.Hide();
        }
    }
}
