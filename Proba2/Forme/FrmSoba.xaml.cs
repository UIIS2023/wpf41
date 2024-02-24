using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
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
using System.Windows.Shapes;

namespace Proba2.Forme
{
    /// <summary>
    /// Interaction logic for FrmSoba.xaml
    /// </summary>
    public partial class FrmSoba : Window
    {

        SqlConnection konekcija = new SqlConnection();
        Konekcija kon = new Konekcija();
        private bool azuriraj;
        private DataRowView red;
        public FrmSoba()
        {
            InitializeComponent();
            konekcija = kon.KreirajKonekciju();
            unosBrojKreveta.Focus();
        }
        public FrmSoba(bool azuriraj, DataRowView red)
        {
            InitializeComponent();
            konekcija = kon.KreirajKonekciju();
            unosBrojKreveta.Focus();
            this.azuriraj = azuriraj;
            this.red = red;
        }

        private void btnSacuvaj_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                konekcija.Open();

                SqlCommand cmd = new SqlCommand
                {
                    Connection = konekcija
                };
                cmd.Parameters.Add("@brojKreveta", SqlDbType.Int).Value = unosBrojKreveta.Text;
                cmd.Parameters.Add("@idSoba", SqlDbType.Int).Value = unosidSobe.Text;
                if (azuriraj)
                {
                    cmd.Parameters.Add("@id", SqlDbType.Int).Value = red["ID"];
                    cmd.CommandText = @"UPDATE Soba SET brojKreveta=@brojKreveta WHERE idSoba=@id";

                    red = null;
                }
                else {
                    cmd.CommandText = @"Insert into Soba (idSoba, brojKreveta) values(@idSoba, @brojKreveta)"; // Moramo uneti neki broj koji vec ne postoji kao id jer nisam postavio automatsko postavljanje brojeva
                }
                cmd.ExecuteNonQuery();
                cmd.Dispose(); //Oslobodimo 

                this.Close();

            }
            catch (SqlException)
            {
                MessageBox.Show("Probajte sa vecim id-jem ", "Greska", MessageBoxButton.OK, MessageBoxImage.Error);


            }
            finally
            {
                if (konekcija != null)
                {
                    konekcija.Close();
                }

            }
        }

        private void btnOtkazi_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
