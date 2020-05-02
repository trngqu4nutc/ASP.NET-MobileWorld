using Model.DTO;
using Model.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Util
{
    public static class CatalogMapper
    {
        public static Catalog toCatalog(CatalogDTO catalogDTO, Catalog catalog)
        {
            catalog.name = catalogDTO.name;
            catalog.pictureuri = catalogDTO.pictureuri;
            catalog.price = catalogDTO.price;
            catalog.quantity = catalogDTO.quantity;
            catalog.description = catalogDTO.description;
            catalog.content = catalogDTO.content;
            catalog.createdAt = catalogDTO.id > 0 ? catalog.createdAt : DateTime.Now;
            catalog.updatedAt = DateTime.Now;
            catalog.catalogbrandid = catalogDTO.catalogbrandid;
            catalog.catalogtypeid = catalogDTO.catalogtypeid;
            return catalog;
        }
        public static Specification toSpecification(CatalogDTO catalogDTO, Specification specification)
        {
            specification.cpu = catalogDTO.cpu;
            specification.ram = catalogDTO.ram;
            specification.screen = catalogDTO.screen;
            specification.os = catalogDTO.os;
            specification.createdAt = catalogDTO.id > 0 ? specification.createdAt : DateTime.Now;
            specification.updatedAt = DateTime.Now;
            specification.catalogid = catalogDTO.id;
            return specification;
        }
        public static SpecificationsMobile toSpecificationsMobile(MobileDTO mobileDTO, SpecificationsMobile mobile)
        {
            try
            {
                mobile.backcamera = mobileDTO.backcamera;
                mobile.frontcamera = mobileDTO.frontcamera;
                mobile.internalmemory = mobileDTO.internalmemory;
                mobile.memorystick = mobileDTO.memorystick;
                mobile.sim = mobileDTO.sim;
                mobile.batery = mobileDTO.batery;
                mobile.createdAt = mobileDTO.id > 0 ? mobile.createdAt : DateTime.Now;
                mobile.updatedAt = DateTime.Now;
                mobile.catalogid = mobileDTO.id;
                return mobile;
            }
            catch (Exception)
            {
                return null;
            }
            
        }
        public static SpecificationsLaptop toSpecificationsLaptop(LaptopDTO laptopDTO, SpecificationsLaptop laptop)
        {
            try
            {
                laptop.cardscreen = laptopDTO.cardscreen;
                laptop.connector = laptopDTO.connector;
                laptop.harddrive = laptopDTO.harddrive;
                laptop.design = laptopDTO.design;
                laptop.size = laptopDTO.size;
                laptop.release = laptopDTO.release;
                laptop.createdAt = laptopDTO.id > 0 ? laptop.createdAt : DateTime.Now;
                laptop.updatedAt = DateTime.Now;
                laptop.catalogid = laptopDTO.id;
                return laptop;
            }
            catch (Exception)
            {
                return null;
            }
            
        }
    }
}
