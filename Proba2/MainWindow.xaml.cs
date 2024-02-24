using Proba2.Forme;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Proba2
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        #region  Select upiti
        string agentiSelect = @"Select idAgent as ID, ime, prezime, JMBG from Agent";
        string korisniciSelect = "select idKorisnik as ID, ime +' '+ prezime as 'Ime i prezime' , godine, JMBG, adresa, grad, kontakt From Korisnik";
        string dodatnaAktivnostSelect = "select idDodatneAktivnosti as ID, tip as Tip, brojClanova as BrojClanova from Dodatna_Aktivnost";
        string HotelSelect = "select idHotel as ID, tip, idSobe from Hotel join Soba on Hotel.idSobe=Soba.idSoba";
        string TipDestinacijeSelect = "SELECT Tip_Destinacije.idTipDestinacije as ID, Tip_Destinacije.tip, Lokacija.idLokacija FROM Tip_Destinacije JOIN Lokacija ON Tip_Destinacije.idLokacija = Lokacija.idLokacija;";
        string SobaSelect = "select idSoba as ID, brojKreveta from Soba";
        string LokacijaSelect = "select idLokacija as ID, naziv from Lokacija";
        string DestinacijaSelect = @"SELECT idDestinacije as ID, Destinacija.naziv AS NazivDestinacije, Destinacija.vremenski_period, Destinacija.datum, Destinacija.cena, Korisnik.ime AS KorisnikIme, Korisnik.JMBG, Korisnik.prezime AS KorisnikPrezime, Agent.ime AS AgentIme, Agent.prezime AS AgentPrezime, Dodatna_Aktivnost.tip AS TipAktivnosti, Hotel.tip AS NazivHotela,
                                    Tip_Destinacije.tip AS NazivTipaDestinacije FROM Destinacija
                                    JOIN Korisnik ON Destinacija.idKorisnik = Korisnik.idKorisnik
                                    JOIN Agent ON Destinacija.idAgent = Agent.idAgent
                                    JOIN Dodatna_Aktivnost ON Destinacija.idDodatnaAktivnost = Dodatna_Aktivnost.idDodatneAktivnosti
                                    JOIN Hotel ON Destinacija.idHotel = Hotel.idHotel
                                    JOIN Tip_Destinacije ON Destinacija.idTipDestinacije = Tip_Destinacije.idTipDestinacije;";
        string OsiguranjeSelect = "SELECT idOsiguranje as ID, tip FROM Osiguranje JOIN Destinacija ON Osiguranje.idDestinacije = Destinacija.idDestinacije;";
        #endregion

        #region Delete upiti
        string agentiDelete = @"delete from Agent Where idAgent=";                         //nastavlajmo dole u funkciji 
        string korisniciDelete = @"delete from Korisnik Where idKorisnik=";
        string DodatnaAktivnostDelete = @"delete from Dodatna_Aktivnost Where idDodatneAktivnosti=";
        string HotelDelete = @"delete from Hotel Where idHotel=";
        string TipDestinacijeDelete = @"delete from Tip_Destinacije Where idTipDestinacije=";
        string SobaDelete = @"delete from Soba Where idSoba=";
        string LokacijaDelete = @"delete from Lokacija Where idLokacija=";
        string DestinacijaDelete = @"delete from Destinacija Where idDestinacije=";
        string OsiguranjeDelete = @"delete from Osiguranje Where idOsiguranje=";

        #endregion

        #region Select sa uslovima

        string agentiUslov = @"select * from Agent Where idAgent=";                         
        string korisniciUslov = @"select * from Korisnik Where idKorisnik=";
        string DodatnaAkivnostUslov = @"select * from Dodatna_Aktivnost Where idDodatneAktivnosti=";
        string HotelUslov = @"select * from Hotel Where idHotel=";
        string TipDestinacijeUslov = @"select * from Tip_Destinacije Where idTipDestinacije=";
        string SobaUslov = @"select * from Soba Where idSoba=";
        string LokacijaUslov = @"select * from Lokacija Where idLokacija=";
        string DestinacijaUslov = @"select * from Destinacija Where idDestinacije=";
        string OsiguranjeUslov = @"select * from Osiguranje Where idOsiguranje=";


        #endregion


        private Konekcija conn = new Konekcija();
        private SqlConnection konekcija = new SqlConnection();
        string ucitanaTabela;                                                    //Atribut klase kojem cemo kasnije doedliti trenutni upit 
        private bool azuriraj;
        private DataRowView red;


        public MainWindow()
        {
            konekcija = conn.KreirajKonekciju();
            InitializeComponent();
            UcitajPodatke(Pocetni,DestinacijaSelect);
            
        }
        void UcitajPodatke(DataGrid grid,string upitiSelect)         //na osonovu ovog selekt upita znamo koja je tabela ucitana
        {
            try
            {
                konekcija.Open();

                SqlDataAdapter dataAdapter = new SqlDataAdapter(upitiSelect, konekcija);                // ucitavanje iz baze 
                DataTable dataTable = new DataTable();
                dataAdapter.Fill(dataTable);



                ucitanaTabela = upitiSelect;                         // treba nam za opciju izbrisi



                if (grid != null)
                {
                    grid.ItemsSource = dataTable.DefaultView;
                }

            }
            catch (SqlException)
            {
                MessageBox.Show("Neuspesno ucitani podaci", "Greska", MessageBoxButton.OK, MessageBoxImage.Error);

            }
            finally
            {
                if (konekcija != null)
                {
                    konekcija.Close();
                }

            }
        }


        private void btnAgenti_Click(object sender, RoutedEventArgs e)
        {
            UcitajPodatke(Pocetni, agentiSelect);
        }

        private void btnDestinacija_Click(object sender, RoutedEventArgs e)
        {
            UcitajPodatke(Pocetni, DestinacijaSelect);
        }

        private void btnKorisnici_Click(object sender, RoutedEventArgs e)
        {
            UcitajPodatke(Pocetni, korisniciSelect);
        }

        private void btnHoteli_Click(object sender, RoutedEventArgs e)
        {
            UcitajPodatke(Pocetni, HotelSelect);
        }

        private void btnDodatneAktivnosti_Click(object sender, RoutedEventArgs e)
        {
            UcitajPodatke(Pocetni, dodatnaAktivnostSelect);
        }

        private void btnOsiguranje_Click(object sender, RoutedEventArgs e)
        {
            UcitajPodatke(Pocetni, OsiguranjeSelect);
        }

        private void btnTipDestinacije_Click(object sender, RoutedEventArgs e)
        {
            UcitajPodatke(Pocetni, TipDestinacijeSelect);
        }

        private void btnSoba_Click(object sender, RoutedEventArgs e)
        {
            UcitajPodatke(Pocetni, SobaSelect);
        }

        private void btnLokacija_Click(object sender, RoutedEventArgs e)
        {
            UcitajPodatke(Pocetni, LokacijaSelect);
        }



        private void btnDodaj_Click(object sender, RoutedEventArgs e)
        {
            Window prozor;
            if (ucitanaTabela.Equals(korisniciSelect))
            {
                prozor = new FrmKorisnik();                                                      //Otvara se prozor
                prozor.ShowDialog();                                                            // i sledece izvrsavnaje ce poceti tek kada se klikne jedan od sacuva ili otkazi u formi

                UcitajPodatke(Pocetni, korisniciSelect);                                           //Ucitavamo podake polja 
            }
            else if (ucitanaTabela.Equals(agentiSelect))
            {
                prozor = new FrmAgent();                                                      
                prozor.ShowDialog();                                                            

                UcitajPodatke(Pocetni, agentiSelect);

            }
            else if (ucitanaTabela.Equals(DestinacijaSelect)) {
                prozor = new FrmDestinacija();                                                      
                prozor.ShowDialog();                                                            

                UcitajPodatke(Pocetni, DestinacijaSelect);

            } else if (ucitanaTabela.Equals(dodatnaAktivnostSelect)) {
                prozor = new FrmDodatnaAktivnost();                                                      
                prozor.ShowDialog();                                                            

                UcitajPodatke(Pocetni, dodatnaAktivnostSelect);
            } else if (ucitanaTabela.Equals(HotelSelect)) {
                prozor = new FrmHotel();                                                      
                prozor.ShowDialog();                                                            

                UcitajPodatke(Pocetni, HotelSelect);
            }
            else if (ucitanaTabela.Equals(LokacijaSelect))
            {
                prozor = new FrmLokacija();
                prozor.ShowDialog();

                UcitajPodatke(Pocetni, LokacijaSelect);
            }
            else if (ucitanaTabela.Equals(OsiguranjeSelect))
            {
                prozor = new FrmOsiguranje();
                prozor.ShowDialog();

                UcitajPodatke(Pocetni, OsiguranjeSelect);
            }
            else if (ucitanaTabela.Equals(SobaSelect))
            {
                prozor = new FrmSoba();
                prozor.ShowDialog();

                UcitajPodatke(Pocetni, SobaSelect);
            }
            else if (ucitanaTabela.Equals(TipDestinacijeSelect))
            {
                prozor = new FrmTipDestinacije();
                prozor.ShowDialog();

                UcitajPodatke(Pocetni, TipDestinacijeSelect);
            }
        }

        private void Obrisi(DataGrid grid, object deleteUpit)
        {
            try
            {
                konekcija.Open();
                DataRowView red = (DataRowView)grid.SelectedItems[0];                     // sleected itemi u tabeli mi mozemo da imamo samo jedan i zato iz tog niza redova uzimamo red sa indexom jedan

                MessageBoxResult rezultat = MessageBox.Show("Da li ste siguni da hocete da izbriste podatke", "Pitanje", MessageBoxButton.YesNo, MessageBoxImage.Question);
                // Ovde pamtimo sta je korisnik izabro da uradi po otvaranjju prozora 
                if (rezultat == MessageBoxResult.Yes)
                {

                    SqlCommand cmd = new SqlCommand()
                    {
                        Connection = konekcija
                    };
                    cmd.Parameters.Add("@id", SqlDbType.Int).Value = red["ID"];
                    cmd.CommandText = deleteUpit + "@id";
                    cmd.ExecuteNonQuery();  

                }

            }
            catch (ArgumentException)
            {
                MessageBox.Show("Niste selektovali red", "Greska", MessageBoxButton.OK, MessageBoxImage.Error);

            }
            finally
            {
                if (konekcija != null)
                {
                    konekcija.Close();
                }

            }
        }


        private void btnIzbrisi_Click(object sender, RoutedEventArgs e)
        {
            if (ucitanaTabela.Equals(agentiSelect)) {
                Obrisi(Pocetni, agentiDelete);
                UcitajPodatke(Pocetni, agentiSelect);
            }
            else if (ucitanaTabela.Equals(korisniciSelect))
            {
                Obrisi(Pocetni, korisniciDelete);
                UcitajPodatke(Pocetni, korisniciSelect);
            }
            else if (ucitanaTabela.Equals(dodatnaAktivnostSelect))
            {
                Obrisi(Pocetni, DodatnaAktivnostDelete);
                UcitajPodatke(Pocetni, dodatnaAktivnostSelect);
            }
            else if (ucitanaTabela.Equals(HotelSelect))
            {
                Obrisi(Pocetni, HotelDelete);
                UcitajPodatke(Pocetni, HotelSelect);
            }
            else if (ucitanaTabela.Equals(TipDestinacijeSelect))
            {
                Obrisi(Pocetni, TipDestinacijeDelete);
                UcitajPodatke(Pocetni, TipDestinacijeSelect);
            }
            else if (ucitanaTabela.Equals(SobaSelect))
            {
                Obrisi(Pocetni, SobaDelete);
                UcitajPodatke(Pocetni, SobaSelect);
            }
            else if (ucitanaTabela.Equals(LokacijaSelect))
            {
                Obrisi(Pocetni, LokacijaDelete);
                UcitajPodatke(Pocetni, LokacijaSelect);
            }
            else if (ucitanaTabela.Equals(DestinacijaSelect))
            {
                Obrisi(Pocetni, DestinacijaDelete);
                UcitajPodatke(Pocetni, DestinacijaSelect);
            }
            else if (ucitanaTabela.Equals(OsiguranjeSelect))
            {
                Obrisi(Pocetni, OsiguranjeDelete);
                UcitajPodatke(Pocetni, OsiguranjeSelect);
            }

        }



        #region Funkcija popuni formu 

        private void PopuniFormu(object selectUslov)
        {
            try
            {
                konekcija.Open();
                azuriraj = true;
                red = (DataRowView)Pocetni.SelectedItems[0];
                SqlCommand cmd = new SqlCommand
                {
                    Connection = konekcija
                };
                cmd.Parameters.Add("@id", SqlDbType.Int).Value = red["ID"];
                cmd.CommandText = selectUslov + "@id";
                SqlDataReader citac = cmd.ExecuteReader();
                cmd.Dispose();
                if (citac.Read())
                {
                    if (ucitanaTabela.Equals(agentiSelect))
                    {
                        FrmAgent prozorAgent = new FrmAgent(azuriraj, red);
                        prozorAgent.unosIme.Text = citac["ime"].ToString();
                        prozorAgent.unosPrezime.Text = citac["prezime"].ToString();
                        prozorAgent.unosJmbg.Text = citac["JMBG"].ToString();
                        prozorAgent.ShowDialog();

                    }
                    else if (ucitanaTabela.Equals(korisniciSelect))
                    {
                        FrmKorisnik prozorKorisnik = new FrmKorisnik(azuriraj, red);
                        prozorKorisnik.unosIme.Text = citac["ime"].ToString();
                        prozorKorisnik.unosPrezime.Text = citac["prezime"].ToString();
                        prozorKorisnik.unosGodine.Text = citac["godine"].ToString();
                        prozorKorisnik.unosJmbg.Text = citac["JMBG"].ToString();
                        prozorKorisnik.unosAdresa.Text = citac["adresa"].ToString();
                        prozorKorisnik.unosGrad.Text = citac["grad"].ToString();
                        prozorKorisnik.unosKontakt.Text = citac["kontakt"].ToString();
                        prozorKorisnik.ShowDialog();

                    }
                    else if (ucitanaTabela.Equals(dodatnaAktivnostSelect)) {
                        FrmDodatnaAktivnost prozorDodatnaAktinost = new FrmDodatnaAktivnost(azuriraj, red);
                        prozorDodatnaAktinost.unosTip.Text = citac["tip"].ToString();
                        prozorDodatnaAktinost.unosBrojClanova.Text = citac["brojClanova"].ToString();
                        prozorDodatnaAktinost.ShowDialog();
                    }
                    else if (ucitanaTabela.Equals(LokacijaSelect))
                    {
                        FrmLokacija prozorLokacija = new FrmLokacija(azuriraj, red);
                        prozorLokacija.unosNaziv.Text = citac["naziv"].ToString();
                        prozorLokacija.ShowDialog();
                    }
                    else if (ucitanaTabela.Equals(SobaSelect))
                    {
                        FrmSoba prozorSoba = new FrmSoba(azuriraj, red);
                        prozorSoba.unosBrojKreveta.Text = citac["brojKreveta"].ToString();
                        prozorSoba.unosidSobe.Text = citac["idSoba"].ToString();
                        prozorSoba.ShowDialog();
                    }
                    else if (ucitanaTabela.Equals(HotelSelect))
                    {
                        FrmHotel prozorHotel = new FrmHotel(azuriraj, red);
                        prozorHotel.UnosTip.Text = citac["tip"].ToString();
                        prozorHotel.dpSoba.SelectedValue = citac["idSobe"].ToString();
                        prozorHotel.ShowDialog();
                    }
                    else if (ucitanaTabela.Equals(TipDestinacijeSelect))
                    {
                        FrmTipDestinacije prozorTipDestinacije = new FrmTipDestinacije(azuriraj, red);
                        prozorTipDestinacije.UnosidTipDestinacije.Text = citac["idTipDestinacije"].ToString();
                        prozorTipDestinacije.UnosTip.Text = citac["tip"].ToString();
                        prozorTipDestinacije.dpLokacija.SelectedValue = citac["idLokacija"].ToString();
                        prozorTipDestinacije.ShowDialog();
                    }
                    else if (ucitanaTabela.Equals(DestinacijaSelect))
                    {
                        FrmDestinacija prozorDestinacija = new FrmDestinacija(azuriraj, red);
                        prozorDestinacija.unosNaziv.Text = citac["naziv"].ToString();
                        prozorDestinacija.unosCena.Text = citac["cena"].ToString();
                        prozorDestinacija.dpDatum.SelectedDate = (DateTime)citac["datum"];
                        prozorDestinacija.dpKorisnik.SelectedValue = citac["idKorisnik"].ToString();
                        prozorDestinacija.dpAgent.SelectedValue = citac["idAgent"].ToString();
                        prozorDestinacija.dpTipDestinacije.SelectedValue = citac["idTipDestinacije"].ToString();
                        prozorDestinacija.dpHotel.SelectedValue = citac["idHotel"].ToString();

                        prozorDestinacija.unosTrajanje.Text = citac["vremenski_period"].ToString();
                        prozorDestinacija.dpDodatnaAktivnost.SelectedValue = citac["idDodatnaAktivnost"].ToString();

                        prozorDestinacija.ShowDialog();


                        
                    }
                    else if (ucitanaTabela.Equals(OsiguranjeSelect))
                    {
                        FrmOsiguranje prozorOsiguranje = new FrmOsiguranje(azuriraj, red);
                        prozorOsiguranje.UnosTip.Text = citac["tip"].ToString();
                        prozorOsiguranje.dpDestinacija.SelectedValue = citac["idDestinacije"].ToString();
                        prozorOsiguranje.ShowDialog();
                    }


                }
            }
            catch (ArgumentOutOfRangeException)
            {
                MessageBox.Show("Niste selektovali red!", "Greska", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            finally
            {
                if (konekcija != null)
                {
                    konekcija.Close();
                }
            }
        }


        #endregion 

        private void btnIzmeni_Click(object sender, RoutedEventArgs e)
        {
            if (ucitanaTabela.Equals(agentiSelect))
            {
                PopuniFormu(agentiUslov);
                UcitajPodatke(Pocetni,agentiSelect);

            }else if (ucitanaTabela.Equals(korisniciSelect))
            {
                PopuniFormu(korisniciUslov);
                UcitajPodatke(Pocetni, korisniciSelect);
            }
            else if (ucitanaTabela.Equals(dodatnaAktivnostSelect))
            {
                PopuniFormu(DodatnaAkivnostUslov);
                UcitajPodatke(Pocetni, dodatnaAktivnostSelect);

            }
            else if (ucitanaTabela.Equals(LokacijaSelect))
            {
                PopuniFormu(LokacijaUslov);
                UcitajPodatke(Pocetni, LokacijaSelect);

            }
            else if (ucitanaTabela.Equals(SobaSelect))
            {
                PopuniFormu(SobaUslov);
                UcitajPodatke(Pocetni, SobaSelect);

            }
            else if (ucitanaTabela.Equals(HotelSelect))
            {
                PopuniFormu(HotelUslov);
                UcitajPodatke(Pocetni, HotelSelect);

            }
            else if (ucitanaTabela.Equals(TipDestinacijeSelect))
            {
                PopuniFormu(TipDestinacijeUslov);
                UcitajPodatke(Pocetni, TipDestinacijeSelect);

            }
            else if (ucitanaTabela.Equals(DestinacijaSelect))
            {
                PopuniFormu(DestinacijaUslov);
                UcitajPodatke(Pocetni, DestinacijaSelect);

            }
            else if (ucitanaTabela.Equals(OsiguranjeSelect))
            {
                PopuniFormu(OsiguranjeUslov);
                UcitajPodatke(Pocetni, OsiguranjeSelect);

            }

        }
    }
}
