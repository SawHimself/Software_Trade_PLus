using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Software_Trade_PLus.Models;

namespace Software_Trade_PLus.ViewModel
{
    class HomeVM
    {
        public HomeVM()
        {
            //Для загрузки базы данных при включении
            DatabaseManagement.ConnectDatabase();
        }
    }
}
