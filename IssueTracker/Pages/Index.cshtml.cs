using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace IssueTracker.Pages;

public class IndexModel : PageModel
{
    public List<IssueSummaryViewModel> Issues;
    private readonly ILogger<IndexModel> _logger;
    private readonly IMapper _mapper;
    private readonly IssueService _issueService;
    private readonly UserService _userService;
    public IEnumerable<SelectListItem> AvailableAssignees { get; set; }

    [BindProperty(SupportsGet = true)]
    public FilterInputModel FilterInput { get; set; }

    public IndexModel(ILogger<IndexModel> logger, IMapper mapper, IssueService issueService, UserService userService)
    {
        _logger = logger;
        _mapper = mapper;
        _issueService = issueService;
        _userService = userService;
    }

    public async void OnGet()
    {
        Issues = await _issueService.GetIssues(FilterInput);
        AvailableAssignees = (await _userService.GetUsersAsync()).Select(x => new SelectListItem { Value = x.Id.ToString(), Text = x.UserName });
    }

    public class FilterInputModel
    {
        public string? Query { get; set; }
        public int? AssigneeId { get; set; }
        public Issue.IssueStatus? IssueStatus { get; set; }
        [DataType(DataType.Date)]
        public DateTime? StartDate { get; set; }
        [DataType(DataType.Date)]
        public DateTime? EndDate { get; set; }
    }
}
