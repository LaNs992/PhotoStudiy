using PhotoStudiy.Context.Contracts.Models;
using PhotoStudiy.Services.Contracts.ModelReqest;
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
    public static class TestDataGenerator
    {
        static public Photogragh Photogragh(Action<Photogragh>? settings = null)
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
        static internal PhotographModel PhotograghModel(Action<PhotographModel>? settings = null)
        {
            var result = new PhotographModel
            {
                Id = Guid.NewGuid(),
                Name = $"{Guid.NewGuid():N}",
                LastName = $"{Guid.NewGuid():N}",
                Number = "891232333"
            };

            settings?.Invoke(result);
            return result;
        }

        static internal PhotoSetModel PhotoSetModel(Action<PhotoSetModel>? settings = null)
        {
            var result = new PhotoSetModel
            {
                Id= Guid.NewGuid(),
                Name = $"{Guid.NewGuid():N}",
                Description = $"{Guid.NewGuid():N}",
                Price = $"33333"
            };

            settings?.Invoke(result);
            return result;
        }

        static internal ProductModel ProductModel(Action<ProductModel>? settings = null)
        {
            var result = new ProductModel
            {
                Id = Guid.NewGuid(),
                Name = $"Книга печать",
                Price = $"печать",
                Amount = 34
            };

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

        static internal RecvisitModel RecvisitModel(Action<RecvisitModel>? settings = null)
        {
            var result = new RecvisitModel
            {
                Id = Guid.NewGuid(),
                Name = $"{Guid.NewGuid():N}",
                Description = $"{Guid.NewGuid():N}",
                Amount = 2
            };
            settings?.Invoke(result);
            return result;
        }
        static internal UslugiModel UslugiModel(Action<UslugiModel>? settings = null)
        {
            var result = new UslugiModel
            {
                Id = Guid.NewGuid(),
                Name = $"{Guid.NewGuid():N}",
                Price = $"3333"

            };
            settings?.Invoke(result);
            return result;
        }
        static public DogovorRequestModel DogovorRequestModel(Action<DogovorRequestModel>? settings = null)
        {
            var result = new DogovorRequestModel
            {
                Id = Guid.NewGuid(),
                Date = DateTimeOffset.Now.AddDays(1),
                Price = 12222
            };

            settings?.Invoke(result);
            return result;
        }
    }
}
