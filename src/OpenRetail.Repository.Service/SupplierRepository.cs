/**
 * Copyright (C) 2017 Kamarudin (http://coding4ever.net/)
 *
 * Licensed under the Apache License, Version 2.0 (the "License"); you may not
 * use this file except in compliance with the License. You may obtain a copy of
 * the License at
 *
 * http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS, WITHOUT
 * WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied. See the
 * License for the specific language governing permissions and limitations under
 * the License.
 *
 * The latest version of this file can be found at https://github.com/rudi-krsoftware/open-retail
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Dapper.Contrib.Extensions;
using OpenRetail.Model;
using OpenRetail.Repository.Api;
 
namespace OpenRetail.Repository.Service
{        
    public class SupplierRepository : ISupplierRepository
    {
        private IDapperContext _context;

        public SupplierRepository(IDapperContext context)
        {
            this._context = context;
        }

        public Supplier GetByID(string id)
        {
            Supplier obj = null;

            try
            {
                obj = _context.db.Get<Supplier>(id);
            }
            catch
            {
            }

            return obj;
        }

        public IList<Supplier> GetByName(string name)
        {
            IList<Supplier> oList = new List<Supplier>();

            try
            {
                oList = _context.db.GetAll<Supplier>()
                                .Where(f => f.nama_supplier.ToLower().Contains(name.ToLower()))
                                .OrderBy(f => f.nama_supplier)
                                .ToList();
            }
            catch
            {
            }            

            return oList;
        }

        public IList<Supplier> GetAll()
        {
            IList<Supplier> oList = new List<Supplier>();

            try
            {
                oList = _context.db.GetAll<Supplier>()
                                .OrderBy(f => f.nama_supplier)
                                .ToList();
            }
            catch
            {
            }

            return oList;
        }

        public int Save(Supplier obj)
        {
            var result = 0;

            try
            {
                obj.supplier_id = _context.GetGUID();

                _context.db.Insert<Supplier>(obj);
                result = 1;
            }
            catch
            {
            }

            return result;
        }

        public int Update(Supplier obj)
        {
            var result = 0;

            try
            {
                result = _context.db.Update<Supplier>(obj) ? 1 : 0;
            }
            catch
            {
            }

            return result;
        }

        public int Delete(Supplier obj)
        {
            var result = 0;

            try
            {
                result = _context.db.Delete<Supplier>(obj) ? 1 : 0;
            }
            catch
            {
            }

            return result;
        }
    }
}     
