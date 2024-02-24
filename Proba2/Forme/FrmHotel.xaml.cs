﻿using System;
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
    /// Interaction logic for FrmHotel.xaml
    /// </summary>
    public partial class FrmHotel : Window
    {
        SqlConnection konekcija = new SqlConnection();
        Konekcija kon = new Konekcija();
        private bool azuriraj;
        private DataRowView red;

        public FrmHotel()
        {
            InitializeComponent();
            konekcija = kon.KreirajKonekciju();
            UnosTip.Focus();

            // U konstruktor stavljamo samo popunjavanje padajuce liste 
            try
            {
                konekcija.Open();
                string vratiSobu = @"select idSoba, brojKreveta from Soba";
                SqlDataAdapter daSoba = new SqlDataAdapter(vratiSobu, konekcija); // koju komandu i nad kojom bazom(koja je definisana u parametrima u konekcije)
                DataTable dtSoba = new DataTable();
                daSoba.Fill(dtSoba);
                dpSoba.ItemsSource = dtSoba.DefaultView;
                daSoba.Dispose();
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
        public FrmHotel(bool azuriraj,DataRowView red)
        {
            InitializeComponent();
            konekcija = kon.KreirajKonekciju();
            UnosTip.Focus();
            this.azuriraj = azuriraj;
            this.red = red;
            
            try
            {
                konekcija.Open();
                string vratiSobu = @"select idSoba, brojKreveta from Soba";
                SqlDataAdapter daSoba = new SqlDataAdapter(vratiSobu, konekcija); 
                DataTable dtSoba = new DataTable();
                daSoba.Fill(dtSoba);
                dpSoba.ItemsSource = dtSoba.DefaultView;
                daSoba.Dispose();
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

                SqlCommand cmd = new SqlCommand  // Kreiramo objekat klase sqlcomand
                {
                    Connection = konekcija
                };

                cmd.Parameters.Add("@tip", SqlDbType.NChar).Value = UnosTip.Text; //Definisime parametre svaki paramatar odgovara jedom atributu iz baze podataka

                cmd.Parameters.Add("@idSoba", SqlDbType.Int).Value = dpSoba.SelectedValue;   //Padajuca lista selected value po kojoj znamo sta smo selecetovali 

                if (azuriraj)
                {
                    cmd.Parameters.Add("@id", SqlDbType.Int).Value = red["ID"];
                    cmd.CommandText = @"UPDATE Hotel SET tip=@tip,idSobe=@idSoba WHERE idHotel=@id";

                    red = null;

                }
                else {
                    cmd.CommandText = @"Insert into Hotel (tip, idSobe) values(@tip, @idSoba)";       // Unsimo parametre u bazu podataka    U koju kolonu hocemo i sta hocemo 
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
