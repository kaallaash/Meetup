using Newtonsoft.Json;
using System.Net.Http.Json;
using System.Net;
using System.Text;
using Xunit;
using static Meetup.API.Tests.Entities.TestEventEntity;
using static Meetup.API.Tests.ViewModels.TestEventViewModel;
using static Meetup.API.Tests.ViewModels.TestChangeEventViewModel;
using static Meetup.API.Tests.Constants.ApiTestsConstants;
using static Meetup.API.Tests.Helpers.ClientBuilder;
using Meetup.API.ViewModels;

namespace Meetup.API.Tests;

public class EventControllerTests
{
    [Fact]
    public async Task Get_ReturnEventViewModel()
    {
        await using var application = new MeetupApi();
        var client = await CreateEventClient(application);

        foreach (var validEventEntity in GetValidEventEntitiesWithId())
        {
            var responseEventViewModel =
                await client.GetFromJsonAsync<EventViewModel>($"{EventPath}/{validEventEntity.Id}");

            Assert.Equal(validEventEntity.Id, responseEventViewModel?.Id);
        }
    }

    [Fact]
    public async Task GetAll_ReturnsEventViewModels()
    {
        await using var application = new MeetupApi();
        var client = await CreateEventClient(application);

        var responseEventViewModels = await client.GetFromJsonAsync<IEnumerable<EventViewModel>>(EventPath);

        Assert.Equal(GetValidCreatedEventEntities().Count(), responseEventViewModels?.Count());
    }

    [Fact]
    public async Task Create_ReturnsEventViewModel()
    {
        await using var application = new MeetupApi();
        var client = await CreateEventClient(application);

        var validCreateEventViewModel = GetValidCreateEventViewModel;
        var jsonEventViewModel = JsonConvert.SerializeObject(validCreateEventViewModel);
        var contentEventViewModel = new StringContent(jsonEventViewModel, Encoding.UTF8, "application/json");

        var response = await client.PostAsync(EventPath, contentEventViewModel);
        var responseEventViewModel = await response.Content.ReadAsAsync<EventViewModel>();

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        Assert.Equal(validCreateEventViewModel.Title, responseEventViewModel?.Title);
    }

    [Fact]
    public async Task Update_ReturnsEventViewModel()
    {
        await using var application = new MeetupApi();
        var client = await CreateEventClient(application);

        var updateEventViewModel = GetValidChangeEventViewModel;
        updateEventViewModel.Title += "Update";

        var jsonEventViewModel = JsonConvert.SerializeObject(updateEventViewModel);
        var contentEventViewModel = new StringContent(jsonEventViewModel, Encoding.UTF8, "application/json");

        var response = await client.PutAsync($"{EventPath}/{GetValidEventViewModel.Id}", contentEventViewModel);
        var responseEventViewModel = await response.Content.ReadAsAsync<EventViewModel>();

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        Assert.Equal(updateEventViewModel.Title, responseEventViewModel?.Title);
    }

    [Fact]
    public async Task Delete_ReturnsEventsCount()
    {
        await using var application = new MeetupApi();
        var client = await CreateEventClient(application);

        var responseDelete = await client.DeleteAsync($"{EventPath}/{GetValidEventViewModel.Id}");
        var responseEventViewModels = await client.GetFromJsonAsync<IEnumerable<EventViewModel>>(EventPath);

        Assert.Equal(HttpStatusCode.OK, responseDelete.StatusCode);
        Assert.Equal(GetValidCreatedEventEntities().Count() - 1, responseEventViewModels?.Count());
    }
}