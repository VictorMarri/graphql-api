using CommanderGQL.Data;
using CommanderGQL.GraphQL.Types;
using CommanderGQL.GraphQL.Types.Commands;
using CommanderGQL.GraphQL.Types.Platforms;
using CommanderGQL.Models;
using HotChocolate;
using HotChocolate.AspNetCore.Subscriptions;
using HotChocolate.Data;
using HotChocolate.Subscriptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CommanderGQL.GraphQL
{
    public class Mutation
    {
        [UseDbContext(typeof(AppDbContext))]
        public async Task<AddPlatformPayload> AddPlatformAsync(
            AddPlatformInput input,
            [ScopedService] AppDbContext context,
            [Service] ITopicEventSender eventSender,
            CancellationToken cancellationToken)

        {
            var platform = new Platform
            {
                Name = input.Name
            };

            context.Platforms.Add(platform);

            await context.SaveChangesAsync(cancellationToken);

            await eventSender.SendAsync(nameof(Subscription.OnPlatformAdded), platform, cancellationToken);

            return new AddPlatformPayload(platform);
        }

        [UseDbContext(typeof(AppDbContext))]
        public async Task<AddCommandPayload> AddCommandAsync(AddCommandInput input, 
                                               [ScopedService] AppDbContext context)
        {
            var command = new Command
            {
                HowTo = input.HowTo,
                CommandLine = input.CommandLine,
                PlatformId = input.PlatformId
            };

            context.Commands.Add(command);

            await context.SaveChangesAsync();

            return new AddCommandPayload(command);
        }
    }
}
