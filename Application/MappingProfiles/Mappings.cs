﻿using Application.Models;
using AutoMapper;
using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.MappingProfiles
{
    public class Mappings : Profile
    {
        public Mappings()
        {
            CreateMap<NewProperty, Property>();
            CreateMap<Property, PropertyDTO>();
            CreateMap<NewImage, Image>();
            CreateMap<Image, ImageDTO>();
        }
    }
}
