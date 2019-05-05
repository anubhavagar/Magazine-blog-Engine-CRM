using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AapkiMagazine.DataLayer
{
    public class DataAccessBase
    {
        #region Member Variables

        protected AppDbContext _AppKiMagazineDbContext;

        #endregion

        #region Properties

        /// <summary>
        /// Gets AppKiMagazineDbContext
        /// </summary>
        protected internal AppDbContext AppKiMagazineDbContext
        {
            get
            {
                if (_AppKiMagazineDbContext == null)
                {
                    _AppKiMagazineDbContext = new AppDbContext();
                }
                return _AppKiMagazineDbContext;
            }
        }

        #endregion

        //#region Methods


        ///// <summary>
        ///// Dispose
        ///// </summary>
        //protected void Dispose()
        //{
        //    if (_AppKiMagazineDbContext != null)
        //    {
        //        _AppKiMagazineDbContext.Dispose();
        //    }
        //}

        //#endregion
    }
}