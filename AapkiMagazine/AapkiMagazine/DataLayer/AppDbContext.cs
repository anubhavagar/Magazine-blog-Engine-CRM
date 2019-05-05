using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
//using AapkiMagazine.DataEntity.Dev;
using AapkiMagazine.DataEntity.prod;

namespace AapkiMagazine.DataLayer
{
    public class AppDbContext : DbContext
    {

        #region Member Variables

        /// <summary>
        /// Member Variable _DbEntities
        /// </summary>


        protected maihuaam_databaseEntities _DbEntities = null;

        #endregion

        #region Properties

        /// <summary>
        /// Gets DbObjectContext
        /// </summary>

        public maihuaam_databaseEntities DbObjectContext
        {
            get
            {
                if (_DbEntities == null)
                {
                    _DbEntities = new maihuaam_databaseEntities();
                }
                return (_DbEntities);
            }
        }

        #endregion

        //#region Methods

        ///// <summary>
        ///// Dispose
        ///// </summary>
        ///// <param name="disposing"></param>
        //protected override void Dispose(bool disposing)
        //{
        //    if (_DbEntities != null)
        //    {
        //        _DbEntities.Dispose();
        //    }
        //    base.Dispose(disposing);
        //}

        ///// <summary>
        ///// Dispose
        ///// </summary>
        //public new void Dispose()
        //{
        //    Dispose(true);
        //}

        //#endregion

    }
}