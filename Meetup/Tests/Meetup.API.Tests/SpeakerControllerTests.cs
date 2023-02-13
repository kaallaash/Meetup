using Meetup.API.ViewModels;
using Newtonsoft.Json;
using System.Net;
using System.Net.Http.Json;
using System.Text;
using Xunit;
using static Meetup.API.Tests.Helpers.ClientBuilder;
using static Meetup.API.Tests.Entities.TestSpeakerEntity;
using static Meetup.API.Tests.ViewModels.TestSpeakerViewModel;
using static Meetup.API.Tests.ViewModels.TestChangeSpeakerViewModel;
using static Meetup.API.Tests.Constants.ApiTestsConstants;

namespace Meetup.API.Tests;

public class SpeakerControllerTests
{
    [Fact]
    public async Task Get_ReturnSpeakerViewModel()
    {
        await using var application = new MeetupApi();
        var client = await CreateSpeakerClient(application);

        foreach (var validSpeakerEntity in GetValidSpeakerEntitiesWithId())
        {
            var responseSpeakerViewModel =
                await client.GetFromJsonAsync<SpeakerViewModel>($"{SpeakerPath}/{validSpeakerEntity.Id}");

            var check = responseSpeakerViewModel;

            Assert.Equal(validSpeakerEntity.Id, responseSpeakerViewModel?.Id);
        }
    }

    [Fact]
    public async Task GetAll_ReturnsSpeakerViewModels()
    {
        await using var application = new MeetupApi();
        var client = await CreateSpeakerClient(application);

        var responseSpeakerViewModels = await client.GetFromJsonAsync<IEnumerable<SpeakerViewModel>>(SpeakerPath);

        Assert.Equal(GetValidSpeakerEntitiesWithId().Count(), responseSpeakerViewModels?.Count());
    }

    [Fact]
    public async Task Create_ReturnsSpeakerViewModel()
    {
        await using var application = new MeetupApi();
        var client = await CreateSpeakerClient(application);

        var validCreatedSpeakerViewModel = GetValidChangeSpeakerViewModel;
        var jsonSpeakerViewModel = JsonConvert.SerializeObject(validCreatedSpeakerViewModel);
        var contentSpeakerViewModel = new StringContent(jsonSpeakerViewModel, Encoding.UTF8, "application/json");

        var response = await client.PostAsync(SpeakerPath, contentSpeakerViewModel);
        var responseSpeakerViewModel = await response.Content.ReadAsAsync<SpeakerViewModel>();

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        Assert.Equal(validCreatedSpeakerViewModel.Name, responseSpeakerViewModel?.Name);
    }

    [Fact]
    public async Task Update_ReturnsSpeakerViewModel()
    {
        await using var application = new MeetupApi();
        var client = await CreateSpeakerClient(application);

        var updateSpeakerViewModel = GetValidChangeSpeakerViewModel;
        updateSpeakerViewModel.Email += "Updated";

        var jsonSpeakerViewModel = JsonConvert.SerializeObject(updateSpeakerViewModel);
        var contentSpeakerViewModel = new StringContent(jsonSpeakerViewModel, Encoding.UTF8, "application/json");

        var response = await client.PutAsync($"{SpeakerPath}/{GetValidSpeakerViewModel.Id}", contentSpeakerViewModel);
        var responseSpeakerViewModel = await response.Content.ReadAsAsync<SpeakerViewModel>();

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        Assert.Equal(updateSpeakerViewModel.Name, responseSpeakerViewModel?.Name);
    }

    [Fact]
    public async Task Delete_ReturnsSpeakersCount()
    {
        await using var application = new MeetupApi();
        var client = await CreateSpeakerClient(application);

        var responseDelete = await client.DeleteAsync($"{SpeakerPath}/{GetValidSpeakerViewModel.Id}");
        var responseSpeakerViewModels = await client.GetFromJsonAsync<IEnumerable<SpeakerViewModel>>(SpeakerPath);

        Assert.Equal(HttpStatusCode.OK, responseDelete.StatusCode);
        Assert.Equal(GetValidSpeakerEntitiesWithId().Count() - 1, responseSpeakerViewModels?.Count());
    }
}