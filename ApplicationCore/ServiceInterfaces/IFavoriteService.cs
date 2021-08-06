﻿using ApplicationCore.Entities;
using ApplicationCore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.ServiceInterfaces
{
    public interface IFavoriteService
    {
        Task<Favorite> ConfirmFavorite(FavoriteModel model);
        Task<List<Favorite>> GetAllFavorites(int id);
    }
}
