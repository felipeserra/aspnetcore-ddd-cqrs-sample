using System.Threading.Tasks;

using FluentAssertions;

using Ploeh.AutoFixture.Xunit2;

using Sample.Infrastructure;
using Sample.ResourceAccess;

using Xunit;

namespace Sample.IntegrationTests.ResourceAccess
{
    public class SampleRepositoryTests : RepositoryTestBase
    {
        private const string ConnectionString = "data source=(LocalDb)\\v12.0;Database=Sample.Database";
        private readonly SampleRepository _repository;

        public SampleRepositoryTests()
        {
            _repository = new SampleRepository(new ConnectionFactory(ConnectionString));
        }

        public class CreateSample : SampleRepositoryTests
        {
            [Theory]
            [AutoData]
            public async Task Should_Save_With_Correct_Data(Domain.CreateSample command)
            {
                await _repository.CreateSample(command);
                var sample = await _repository.GetSample(new Domain.GetSample { SampleId = command.SampleId });

                sample.SampleAmount.Should().Be(command.SampleAmount);
                sample.SampleId.Should().Be(command.SampleId);
            }
        }

        public class UpdateSample : SampleRepositoryTests
        {
            [Theory]
            [AutoData]
            public async Task Should_Update_With_Correct_Amount(Domain.UpdateSample command)
            {
                await _repository.CreateSample(new Domain.CreateSample
                {
                    SampleId = command.SampleId,
                    SampleAmount = default(double)
                });

                await _repository.UpdateSample(command);

                var sample = await _repository.GetSample(new Domain.GetSample { SampleId = command.SampleId });

                sample.SampleAmount.Should().Be(command.SampleAmount);
                sample.SampleId.Should().Be(command.SampleId);
            }
        }

        public class DeleteSample : SampleRepositoryTests
        {
            [Theory]
            [AutoData]
            public async Task Should_Delete_Sample_If_Sample_Exists(Domain.DeleteSample command)
            {
                await _repository.CreateSample(new Domain.CreateSample
                {
                    SampleId = command.SampleId,
                    SampleAmount = default(double)
                });

                var sample = await _repository.GetSample(new Domain.GetSample { SampleId = command.SampleId });

                sample.SampleId.Should().Be(command.SampleId);

                await _repository.DeleteSample(command);

                sample = await _repository.GetSample(new Domain.GetSample { SampleId = command.SampleId });

                sample.Should().BeNull();
            }
        }

        public class GetSample : SampleRepositoryTests
        {
            [Theory]
            [AutoData]
            public async Task Should_Return_Sample_If_Found(Domain.GetSample query)
            {
                await _repository.CreateSample(new Domain.CreateSample
                {
                    SampleId = query.SampleId,
                    SampleAmount = default(double)
                });

                var sample = await _repository.GetSample(query);

                sample.SampleId.Should().Be(query.SampleId);
            }

            [Theory]
            [AutoData]
            public async Task Should_Return_Null_If_Not_Found(Domain.GetSample query)
            {
                var sample = await _repository.GetSample(query);

                sample.Should().BeNull();
            }
        }
    }
}
