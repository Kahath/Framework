﻿/*
 * This program is free software: you can redistribute it and/or modify
 *  it under the terms of the GNU General Public License as published by
 *  the Free Software Foundation, either version 3 of the License, or
 *  (at your option) any later version.
 *
 *  This program is distributed in the hope that it will be useful,
 *  but WITHOUT ANY WARRANTY; without even the implied warranty of
 *  MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
 *  GNU General Public License for more details.
 *
 *  You should have received a copy of the GNU General Public License
 *  along with this program.  If not, see <http://www.gnu.org/licenses/>.
 */

using InteractivePreGeneratedViews;
using System;
using System.Data.Entity;
using System.IO;
using System.Reflection;

namespace DatabaseFramework.Database.Context
{
    public class DBContextBase : DbContext
    {
        #region Fields

        private string _connectionString;
        private string _directoryPath;

        #endregion

        #region Properties

        public string ConnectionString
        {
            get { return _connectionString; }
            set { _connectionString = value; }
        }

        #endregion

        #region Constructors

        public DBContextBase(string connectionString)
            : base(connectionString)
        {
            ConnectionString = connectionString;
            Configuration.AutoDetectChangesEnabled = false;

            Init();
        }

        #endregion

        #region Methods

        private void Init()
        {
            _directoryPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\" + Assembly.GetEntryAssembly().GetName().Name + @"\" + this.Database.Connection.Database + @"\";

            if (!Directory.Exists(_directoryPath))
            {
                Directory.CreateDirectory(_directoryPath);
            }

            InteractiveViews.SetViewCacheFactory(this,
                new FileViewCacheFactory(_directoryPath + @"\" + this + ".xml"));
        }
        
        public async void Save()
        {
            await SaveChangesAsync();
        }

        #endregion
    }
}
