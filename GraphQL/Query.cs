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
        [UseDbContext(typeof(AppDbContext))] //Aqui estou falando que esse metodo a gente vai pegar ele da pool que a gente construiu na instancia do DbContext no startup e devolver tudo quando tiver finalizado
        public IQueryable<Platform> GetPlatform([ScopedService] AppDbContext context) //ScopedService tem a ver com o tempo de vida do contexto que vamos estar usando. Enquanto a gente usar o mesmo contexto esse metodo vai funcionar nas consultas, independentemente se for simultaneamente.
        {
            return context.Platforms;
        }
    }
}
