﻿using Farmalivio_Farmacia.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Farmalivio_Farmacia.ViewModel
{
    public class ProductoViewModel : Paginacion
    {
        public List<vista_producto> Productos { get; set; }
        public string Criterio { get; set; }
    }
}