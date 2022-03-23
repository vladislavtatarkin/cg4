﻿using System.Threading.Tasks;
using BenchmarkDotNet.Attributes;
using CG4.Benchmark.Tests.Preparation;
using CG4.Story;
using CG4.Story.Impl;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace CG4.Benchmark.Tests
{
    [MemoryDiagnoser]
    public class StoryBuilderTests
    {
        private readonly IStoryExecutor _newBuilder;
        private readonly bgTeam.StoryBuilder _oldBuilder;
        private readonly IMediator _mediatr;

        public StoryBuilderTests()
        {
            var collection = new ServiceCollection();
            collection.AddTransient<IStory<TestStoryContext, int>, TestStory>();
            collection.AddTransient<bgTeam.IStory<TestStoryContext, int>, TestStory>();
            collection.AddMediatR(typeof(Program));
            var provider = collection.BuildServiceProvider();

            _newBuilder = new StoryExecutor(provider);
            var factory = new bgTeam.Impl.StoryFactory(provider);
            _oldBuilder = new bgTeam.StoryBuilder(factory);
            _mediatr = provider.GetRequiredService<IMediator>();
        }

        [Benchmark]
        public Task<int> NewBuilder() => _newBuilder.Execute(new TestStoryContext());

        [Benchmark]
        public Task<int> OldBuilder() => _oldBuilder.Build(new TestStoryContext()).ReturnAsync<int>();

        [Benchmark]
        public Task<int> MediatR() => _mediatr.Send(new TestStoryContext());
    }
}