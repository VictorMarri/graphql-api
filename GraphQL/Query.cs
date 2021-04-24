using CommanderGQL.Data;
using CommanderGQL.Models;
using HotChocolate;
using HotChocolate.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CommanderGQL.GraphQL
{
    public class Query
    {
        [UseDbContext(typeof(AppDbContext))] //Implementando um context Multi-Thread, que vai suportar varias consultas ao mesmo tempo
        [UseProjection] //Para mostrarmos objetos filhos
        public IQueryable<Platform> GetPlatform([ScopedService] AppDbContext context) //ScopedService tem a ver com o tempo de vida do contexto que vamos estar usando. Enquanto a gente usar o mesmo contexto esse metodo vai funcionar nas consultas, independentemente se for simultaneamente.
        {
            return context.Platforms;
        }

        [UseDbContext(typeof(AppDbContext))] //Multi Thread
        [UseProjection]
        public IQueryable<Command> GetCommand([ScopedService] AppDbContext context) 
        {
            return context.Commands;
        }
    }
}
