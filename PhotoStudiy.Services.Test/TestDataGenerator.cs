using PhotoStudiy.Context.Contracts.Models;
using PhotoStudiy.Services.Contracts.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace PhotoStudiy.Repositories.Test
{
    internal static class TestDataGenerator
    {
        static internal Photogragh Photogragh(Action<Photogragh>? settings = null)
        {
            var result = new Photogragh
            {
                Name = $"{Guid.NewGuid():N}",
                LastName = $"{Guid.NewGuid():N}",
                Number = $"{Guid.NewGuid():N}"
            };
            result.BaseAuditSetParamtrs();

            settings?.Invoke(result);
            return result;
        }

        static internal PhotoSet PhotoSet(Action<PhotoSet>? settings = null)
        {
            var result = new PhotoSet
            {
                Name = $"{Guid.NewGuid():N}",
                Description = $"{Guid.NewGuid():N}",
                Price  = $"{Guid.NewGuid():N}"
            };
            result.BaseAuditSetParamtrs();

            settings?.Invoke(result);
            return result;
        }

        static internal Product Product(Action<Product>? settings = null)
        {
            var result = new Product
            {
                Name = $"{Guid.NewGuid():N}",
                Price = $"{Guid.NewGuid():N}",
                Amount = 34
            };
            result.BaseAuditSetParamtrs();

            settings?.Invoke(result);
            return result;
        }

        static internal Client Client(Action<Client>? settings = null)
        {
            var result = new Client
            {
                Name = $"{Guid.NewGuid():N}",
                LastName = $"{Guid.NewGuid():N}",    
                Number = $"{Guid.NewGuid():N}"
            };
            result.BaseAuditSetParamtrs();
            settings?.Invoke(result);
            return result;
        }

        static internal Recvisit Recvisit(Action<Recvisit>? settings = null)
        {
            var result = new Recvisit
            {
                Name = $"{Guid.NewGuid():N}",
                Description = $"{Guid.NewGuid():N}",
                Amount = 2
            };
            result.BaseAuditSetParamtrs();
            settings?.Invoke(result);
            return result;
        }
        static internal Uslugi Uslugi(Action<Uslugi>? settings = null)
        {
            var result = new Uslugi
            {
                Name = $"{Guid.NewGuid():N}",
                Price = $"{Guid.NewGuid():N}"
                
            };
            result.BaseAuditSetParamtrs();
            settings?.Invoke(result);
            return result;
        }
        static internal Dogovor Dogovor(Action<Dogovor>? settings = null)
        {
            var result = new Dogovor
            {
                Date = DateTimeOffset.UtcNow,
                Price = 12300
            };
            result.BaseAuditSetParamtrs();

            settings?.Invoke(result);
            return result;
        }
        static internal Photogragh PhotograghModel(Action<Photogragh>? settings = null)
        {
            var result = new Photogragh
            {
                Name = $"{Guid.NewGuid():N}",
                LastName = $"{Guid.NewGuid():N}",
                Number = $"{Guid.NewGuid():N}"
            };
            result.BaseAuditSetParamtrs();

            settings?.Invoke(result);
            return result;
        }

        static internal PhotoSet PhotoSetModel(Action<PhotoSet>? settings = null)
        {
            var result = new PhotoSet
            {
                Name = $"{Guid.NewGuid():N}",
                Description = $"{Guid.NewGuid():N}",
                Price = $"{Guid.NewGuid():N}"
            };
            result.BaseAuditSetParamtrs();

            settings?.Invoke(result);
            return result;
        }

        static internal Product ProductModel(Action<Product>? settings = null)
        {
            var result = new Product
            {
                Name = $"{Guid.NewGuid():N}",
                Price = $"{Guid.NewGuid():N}",
                Amount = 34
            };
            result.BaseAuditSetParamtrs();

            settings?.Invoke(result);
            return result;
        }

        static internal ClientModel ClientModel(Action<ClientModel>? settings = null)
        {
            var result = new ClientModel
            {
                Id= Guid.NewGuid(),
                Name = $"{Guid.NewGuid():N}",
                LastName = $"{Guid.NewGuid():N}",
                Number = "891232333"
            };
          
            settings?.Invoke(result);
            return result;
        }

        static internal Recvisit RecvisitModel(Action<Recvisit>? settings = null)
        {
            var result = new Recvisit
            {
                Name = $"{Guid.NewGuid():N}",
                Description = $"{Guid.NewGuid():N}",
                Amount = 2
            };
            result.BaseAuditSetParamtrs();
            settings?.Invoke(result);
            return result;
        }
        static internal Uslugi UslugiModel(Action<Uslugi>? settings = null)
        {
            var result = new Uslugi
            {
                Name = $"{Guid.NewGuid():N}",
                Price = $"{Guid.NewGuid():N}"

            };
            result.BaseAuditSetParamtrs();
            settings?.Invoke(result);
            return result;
        }
        static internal Dogovor DogovorModel(Action<Dogovor>? settings = null)
        {
            var result = new Dogovor
            {
                Date = DateTimeOffset.UtcNow,
                Price = 12300
            };
            result.BaseAuditSetParamtrs();

            settings?.Invoke(result);
            return result;
        }
    }
}
