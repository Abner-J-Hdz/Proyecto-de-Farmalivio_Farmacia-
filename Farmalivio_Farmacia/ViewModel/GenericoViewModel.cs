﻿using Farmalivio_Farmacia.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Farmalivio_Farmacia.ViewModel
{
    public class GenericoViewModel:Paginacion
    {
        public List<generico> Genericos { get; set; }
    }
}