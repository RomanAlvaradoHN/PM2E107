using PM2E107.Controllers;
using PM2E107.Views;
using SQLite;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;


namespace PM2E107.Models {
    public class Sitio {
        private List<string> invalidData = new List<string>();
        private byte[] foto;
        private double latitud;
        private double longitud;
        private string descripcion;



        public Sitio() {

        }


        public Sitio(byte[] foto, Location locacion, string descripcion) {
            this.Foto = foto;
            this.Locacion = locacion;
            this.Descripcion = descripcion;
        }




        public List<string> GetDatosInvalidos() {
            return this.invalidData;
        }





        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }



        [Column("Foto")]
        public byte[] Foto {
            get { return this.foto; }

            set {
                if (value != null && value.Length > 0) {
                    this.foto = value;
                } else {
                    this.invalidData.Add("Foto");
                }
            }
        }




        [Ignore]
        public Location Locacion {
            get { 
                return new Location(this.Latitud, this.Longitud);
            }

            set {
                if (value.Longitude != 0.0 && value.Latitude != 0.0) {
                    this.Latitud = value.Latitude;
                    this.Longitud = value.Longitude;
                } else {
                    this.invalidData.Add("Locacion");
                }
            }
        }


        [Column("Longitud")]
        public double Longitud {
            get { return this.longitud; }

            set {
                if (value != 0.0) {
                    this.longitud = value;
                } else {
                    this.invalidData.Add("Longitud");
                }
            }
        }


        [Column("Latitud")]
        public double Latitud {
            get { return this.latitud; }

            set {
                if (value != 0.0) {
                    this.latitud = value;
                } else {
                    this.invalidData.Add("Latitud");
                }
            }
        }





        [Column("Descripcion")]
        public string Descripcion {
            get { return this.descripcion; }

            set {
                if (!string.IsNullOrEmpty(value)) {
                    this.descripcion = value;
                } else {
                    this.invalidData.Add("Descripcion");
                }
            }
        }


    }
}
