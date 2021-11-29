using System;
using System.Collections.Generic;
using Dominio;
using System.Linq;

namespace Persistencia
{
    public class RepoMigrante : IMigrante
    {
        private readonly AppContext _context;

        public RepoMigrante(AppContext context)
        {
            _context = context;
        }

        bool IMigrante.CreateMigrante(Migrante migrante)
        {
            bool creado = false;
            try
            {
                _context.Migrantes.Add(migrante) ;
                _context.SaveChanges();
                creado = true;
            }
            catch (System.Exception _ex)
            {
                Console.WriteLine("ERROR USUARIO: " + _ex.Message);
                return creado;
            }
            return creado;
        }

        List<Migrante> IMigrante.ListarMigrantesList()
        {
            return _context.Migrantes.ToList();
        }
        bool IMigrante.EditMigrante(Migrante migrante)
        {
            bool actualizado = false;
            try
            {
                 //var munEncontrado = _context.Migrantes.Find(migrante.Id);
                 var migEncontrado = _context.Migrantes.FirstOrDefault(a => a.userName == migrante.userName);
                 if(migEncontrado != null)
                 {
                    migEncontrado.Nombre = migrante.Nombre;
                    migEncontrado.Apellidos= migrante.Apellidos;
                    migEncontrado.Tipo_Documento= migrante.Tipo_Documento;
                    migEncontrado.Documento= migrante.Documento;
                    migEncontrado.Pais= migrante.Pais;
                    migEncontrado.FechaNacimiento= migrante.FechaNacimiento;
                    migEncontrado.Direccion_Electronica= migrante.Direccion_Electronica;
                    migEncontrado.Telefono= migrante.Telefono;
                    migEncontrado.Dir_Actual= migrante.Dir_Actual;
                    migEncontrado.Ciudad= migrante.Ciudad;
                    migEncontrado.SituacionLab= migrante.SituacionLab;
                    _context.SaveChanges();
                    actualizado = true;
                 }
            }
            catch (System.Exception)
            {
                return actualizado;
            }
            return actualizado;
        }
        bool IMigrante.DeleteMigrante(string docMigrante)
        {
            bool eliminado = false;
            try
            {
                 var migrante = _context.Migrantes.FirstOrDefault(d => d.Documento == docMigrante);
                 if(migrante != null)
                 {
                     _context.Migrantes.Remove(migrante);
                     _context.SaveChanges();
                     eliminado = true;
                 }
            }
            catch (System.Exception)
            {
                return eliminado;
            }
            return eliminado;
        }
        Migrante IMigrante.ReadMigrante(string docMigrante)
        {
            Migrante migrante = _context.Migrantes.FirstOrDefault(d => d.Documento == docMigrante);
            return migrante;
        }

        Migrante IMigrante.FindByUserName(string _userName)
        {
            Migrante migrante = _context.Migrantes.FirstOrDefault(d => d.userName == _userName);
            return migrante;
        }

        IEnumerable<Migrante> IMigrante.ListarMigrantes()
        {
            return _context.Migrantes;
        }
    }
}