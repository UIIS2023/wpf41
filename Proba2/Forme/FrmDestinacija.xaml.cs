using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
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
    /// Interaction logic for FrmDestinacija.xaml
    /// </summary>
    public partial class FrmDestinacija : Window
    {

        SqlConnection konekcija = new SqlConnection();
        Konekcija kon = new Konekcija();
        private bool azuriraj;
        private DataRowView red;
        public FrmDestinacija()
        {
            InitializeComponent();
            konekcija = kon.KreirajKonekciju();
            unosNaziv.Focus();

            try
            {
                konekcija.Open();

                string vratiKorisnike = @"select idKorisnik, ime + ' ' + prezime as Korisnik from Korisnik";
                SqlDataAdapter daKorisnik = new SqlDataAdapter(vratiKorisnike, konekcija);
                DataTable dtKorisnik = new DataTable();
                daKorisnik.Fill(dtKorisnik);
                dpKorisnik.ItemsSource = dtKorisnik.DefaultView;
                dtKorisnik.Dispose();
                daKorisnik.Dispose();

                string vratiAgente = @"select idAgent, ime from Agent";
                SqlDataAdapter daAgent = new SqlDataAdapter(vratiAgente, konekcija);
                DataTable dtAgent = new DataTable();
                daAgent.Fill(dtAgent);
                dpAgent.ItemsSource = dtAgent.DefaultView;
                dtAgent.Dispose();
                daAgent.Dispose();

                string vratiTipDestinacije = @"select idTipDestinacije, tip from Tip_Destinacije";
                SqlDataAdapter daTipDestinacije = new SqlDataAdapter(vratiTipDestinacije, konekcija);
                DataTable dtTipDestinacije = new DataTable();
                daTipDestinacije.Fill(dtTipDestinacije);
                dpTipDestinacije.ItemsSource = dtTipDestinacije.DefaultView;
                dtTipDestinacije.Dispose();
                daTipDestinacije.Dispose();

                string vratiHotele = @"select idHotel, tip from Hotel";
                SqlDataAdapter daHotel = new SqlDataAdapter(vratiHotele, konekcija);
                DataTable dtHotel = new DataTable();
                daHotel.Fill(dtHotel);
                dpHotel.ItemsSource = dtHotel.DefaultView;
                dtHotel.Dispose();
                daHotel.Dispose();

                string vratiDodatneAktivnosti = @"select idDodatneAktivnosti, tip from Dodatna_Aktivnost";                    
                SqlDataAdapter daDodatnaAktivnost = new SqlDataAdapter(vratiDodatneAktivnosti, konekcija);
                DataTable dtDodatnaAktivnost = new DataTable();
                daDodatnaAktivnost.Fill(dtDodatnaAktivnost);
                dpDodatnaAktivnost.ItemsSource = dtDodatnaAktivnost.DefaultView;
                dtDodatnaAktivnost.Dispose();
                daDodatnaAktivnost.Dispose();



            }
            catch (SqlException)
            {
                MessageBox.Show("Padajuce liste nisu popnjene", "Greska", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            finally
            {
                if (konekcija != null)
                {
                    konekcija.Close();
                }

            }
        }
        public FrmDestinacija(bool azuriraj,DataRowView red)
        {
            InitializeComponent();
            konekcija = kon.KreirajKonekciju();
            unosNaziv.Focus();
            this.azuriraj = azuriraj;
            this.red = red;

            try
            {
                konekcija.Open();

                string vratiKorisnike = @"select idKorisnik, ime + ' ' + prezime as Korisnik from Korisnik";
                SqlDataAdapter daKorisnik = new SqlDataAdapter(vratiKorisnike, konekcija);
                DataTable dtKorisnik = new DataTable();
                daKorisnik.Fill(dtKorisnik);
                dpKorisnik.ItemsSource = dtKorisnik.DefaultView;
                dtKorisnik.Dispose();
                daKorisnik.Dispose();

                string vratiAgente = @"select idAgent, ime from Agent";
                SqlDataAdapter daAgent = new SqlDataAdapter(vratiAgente, konekcija);
                DataTable dtAgent = new DataTable();
                daAgent.Fill(dtAgent);
                dpAgent.ItemsSource = dtAgent.DefaultView;
                dtAgent.Dispose();
                daAgent.Dispose();

                string vratiTipDestinacije = @"select idTipDestinacije, tip from Tip_Destinacije";
                SqlDataAdapter daTipDestinacije = new SqlDataAdapter(vratiTipDestinacije, konekcija);
                DataTable dtTipDestinacije = new DataTable();
                daTipDestinacije.Fill(dtTipDestinacije);
                dpTipDestinacije.ItemsSource = dtTipDestinacije.DefaultView;
                dtTipDestinacije.Dispose();
                daTipDestinacije.Dispose();

                string vratiHotele = @"select idHotel, tip from Hotel";
                SqlDataAdapter daHotel = new SqlDataAdapter(vratiHotele, konekcija);
                DataTable dtHotel = new DataTable();
                daHotel.Fill(dtHotel);
                dpHotel.ItemsSource = dtHotel.DefaultView;
                dtHotel.Dispose();
                daHotel.Dispose();

                string vratiDodatneAktivnosti = @"select idDodatneAktivnosti, tip from Dodatna_Aktivnost";                    
                SqlDataAdapter daDodatnaAktivnost = new SqlDataAdapter(vratiDodatneAktivnosti, konekcija);
                DataTable dtDodatnaAktivnost = new DataTable();
                daDodatnaAktivnost.Fill(dtDodatnaAktivnost);
                dpDodatnaAktivnost.ItemsSource = dtDodatnaAktivnost.DefaultView;
                dtDodatnaAktivnost.Dispose();
                daDodatnaAktivnost.Dispose();



            }
            catch (SqlException)
            {
                MessageBox.Show("Padajuce liste nisu popnjene", "Greska", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            finally
            {
                if (konekcija != null)
                {
                    konekcija.Close();
                }

            }
        }



        private void btnSacuvaj_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                konekcija.Open();
                DateTime date = (DateTime)dpDatum.SelectedDate; // u traj ispod konekciej ili se vrati na vieo 4.34 sat
                string datum = date.ToString("yyyy-MM-dd", CultureInfo.CurrentCulture);  //moramo da preforamtiramo datum kako bi smo ga pokazlai u list
                SqlCommand cmd = new SqlCommand  // Kreiramo objekat klase sqlcomand
                {
                    Connection = konekcija

                };
                cmd.Parameters.Add("@naziv", SqlDbType.NChar).Value = unosNaziv.Text; 
                cmd.Parameters.Add("@cena", SqlDbType.Decimal).Value = unosCena.Text;
                cmd.Parameters.Add("@trajanje", SqlDbType.Int).Value = unosTrajanje.Text;

                cmd.Parameters.Add("@datum", SqlDbType.Date).Value = datum;

                cmd.Parameters.Add("@idKorisnik", SqlDbType.Int).Value = dpKorisnik.SelectedValue;
                cmd.Parameters.Add("@idAgent", SqlDbType.Int).Value = dpAgent.SelectedValue;
                cmd.Parameters.Add("@idTipDestinacije", SqlDbType.Int).Value = dpTipDestinacije.SelectedValue;
                cmd.Parameters.Add("@idHotel", SqlDbType.Int).Value = dpHotel.SelectedValue;
                cmd.Parameters.Add("@idDodatneAktivnosti", SqlDbType.Int).Value = dpDodatnaAktivnost.SelectedValue;

                if (azuriraj)
                {
                    cmd.Parameters.Add("@id", SqlDbType.Int).Value = red["ID"];
                    cmd.CommandText = @"UPDATE Destinacija SET naziv=@naziv, cena=@cena, idKorisnik=@idKorisnik, datum=@datum, vremenski_period=@trajanje, 
                                            idAgent=@idAgent, idHotel=@idHotel, idTipDestinacije=@idTipDestinacije, idDodatnaAktivnost=@idDodatneAktivnosti WHERE idDestinacije=@id";

                    red = null;
                }
                else {
                    
                    cmd.CommandText = @"Insert into Destinacija (naziv, cena, vremenski_period, datum, idKorisnik, idAgent, idTipDestinacije, idHotel, idDodatnaAktivnost ) values(@naziv, @cena, @trajanje, @datum, @idKorisnik, @idAgent, @idTipDestinacije, @idHotel, @idDodatneAktivnosti )";       

                }

                cmd.ExecuteNonQuery();
                cmd.Dispose(); //Oslobodimo 
                this.Close();
            }
            catch (SqlException)
            {
                MessageBox.Show("Unos nekih parametra nije validan", "Greska", MessageBoxButton.OK, MessageBoxImage.Error);

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
