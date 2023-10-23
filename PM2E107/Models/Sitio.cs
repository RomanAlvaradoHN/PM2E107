using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PM2E107.Models {
    public class Sitio {
        private List<string> invalidData = new List<string>();
        private byte[] foto;
        private string latitud;
        private string longitud;
        private string descripcion;



        public Sitio() {

        }


        public Sitio(byte[] foto, string latitud, string longitud, string descripcion) {
            this.Foto = foto;
            this.Longitud = longitud;
            this.Latitud = latitud;
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




        [Column("Latitud")]
        public string Latitud {
            get { return this.latitud; }

            set {
                if (!string.IsNullOrEmpty(value)) {
                    this.latitud = value;
                } else {
                    this.invalidData.Add("Latitud");
                }
            }
        }



        [Column("Longitud")]
        public string Longitud {
            get { return this.longitud; }

            set {
                if (!string.IsNullOrEmpty(value)) {
                    this.longitud = value;
                } else {
                    this.invalidData.Add("Longitud");
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
