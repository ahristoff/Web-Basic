﻿using System;
using System.Collections.Generic;
using System.Text;

namespace WebServer.GameStoreApplication.Services.Contracts
{
    using System;
    using System.Collections.Generic;
    using Models.Admin;

    public interface IGameService
    {
        void Create(
            string title,
            string description,
            string image,
            decimal price,
            double size,
            string videoId,
            DateTime releaseDate);

        IEnumerable<AdminListGameViewModel> All();
    }
}
