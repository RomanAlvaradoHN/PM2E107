using PM2E107.Models;
using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PM2E107.Controllers {
    public class DBController {

        //DATABASE CONFIGURATION VARIABLES
        //=======================================================================================
        private readonly static string dbFileName = "SitiosAppDB.db3";

        private readonly SQLiteOpenFlags flags = SQLiteOpenFlags.ReadWrite |
                                                 SQLiteOpenFlags.Create |
                                                 SQLiteOpenFlags.SharedCache;

        private readonly string dbPath = Path.Combine(FileSystem.AppDataDirectory, dbFileName);
        //---------------------------------------------------------------------------------------
        private SQLiteAsyncConnection connection;
        //======================================================================================




        public DBController() {
            connection = new SQLiteAsyncConnection(dbPath, flags);
            connection.CreateTableAsync<Sitio>().Wait();
        }












        public async Task<List<Sitio>> SelectAll() {
            return await connection.Table<Sitio>().ToListAsync();
        }


        public async Task<Sitio> SelectById(int id) {
            return await connection.Table<Sitio>().Where(col => col.Id == id).FirstOrDefaultAsync();
        }




        public async Task<int> Insert(Sitio sitio) {
            return await connection.InsertAsync(sitio);
        }





        public async Task<int> Update(Sitio sitio) {
            return await connection.UpdateAsync(sitio);
        }





        public async Task<int> Delete(Sitio sitio) {
            return await connection.DeleteAsync(sitio);
        }

    }
}
