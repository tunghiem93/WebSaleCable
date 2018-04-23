var checkAll = false;
$(document).ready(function () {
    jQuery.validator.methods["date"] = function (value, element) { return true; }
});
/* Toogle check all checkboxes from table or div with given element selector, ex: "#divID", ".tableClass" */
function ToogleCheckAll(e, containElementSelector) {
    checkAll = $(e).prop("checked");
    $(containElementSelector).find("input[type='checkbox']").prop("checked", checkAll);
    if ($(e).prop('id') != 'select-all')
        ToggleBtnDelete();
}

/* Toogle check all checkboxes from table or div with given element selector, ex: "#divID", ".tableClass" */
function IndexCheckAll(containElementSelector, e) {
    if ($(e).hasClass("check-all")) {
        $(containElementSelector).find("input[type='checkbox']").prop("checked", true);
        $(e).removeClass("check-all").addClass("uncheck-all");
    }
    else if ($(e).hasClass("uncheck-all")) {
        $(containElementSelector).find("input[type='checkbox']").prop("checked", false);
        $(e).removeClass("uncheck-all").addClass("check-all");
    }
    ToggleBtnDelete();
}

function ToggleBtnDelete() {

    var totalRow = $(".gridview tbody input[type='checkbox']").length;
    var totalChecked = $(".gridview tbody input[type='checkbox']:checked").length;

    if (totalRow == totalChecked) {
        $("#chb-checkall").prop('checked', true);
    } else {
        $("#chb-checkall").prop('checked', false);

    }
}

function checkItem() {
    var countCheck = $('.employee-items').find("input[type='checkbox']").length;
    var index = 0;
    for (var i = 0; i < countCheck; i++) {
        var item = $('.employee-items').find("input[id='ListEmployees_" + i + "__Checked']")
        if (item.prop('checked')) {
            index++;
        }
    }
    if (index == countCheck) {
        $("#checkAllEmp").prop('checked', true);
    }
    else {
        $("#checkAllEmp").prop('checked', false);
    }
}

function include(scriptUrl) {
    document.write('<script src="' + scriptUrl + '"></script>');
}


/*Call Search - edit*/
/** Global variables **/
var ControllerName;
var SelectingCateID = "0";

var SelectingRoleID = "0";
var SelectingEmpID = "0";

var SelectingPrinterID = "0";
var ItemType = 0;
var listP = "";

// new function 23/07
function CreateAbsoluteUrl(actionName) {
    var getUrl = window.location;
    return BaseUrl + ControllerName + "/" + actionName;
}

//For Area in MVC
function CreateAbsoluteUrlArea(area, actionName) {
    var getUrl = window.location;
    return BaseUrl + area + "/" + ControllerName + "/" + actionName;
}

/** Functions **/
function PreviewImage(e, previewElementID) {
    var oFReader = new FileReader();
    oFReader.readAsDataURL(e.files[0]);

    oFReader.onload = function (oFREvent) {
        document.getElementById(previewElementID).src = oFREvent.target.result;
    };
};


/* Call View or Edit action with given url and show response (html) in .detail-view element */
function ShowViewOrEdit(action) {
    $.ajax({
        url: action,
        beforeSend: function () {
            $(".se-pre-con").show();
        },
        error: function (jqXHR, textStatus, errorThrown) {
            alert(errorThrown);
            $(".se-pre-con").hide();
        },
        success: function (html) {
            $(".se-pre-con").hide();
            ShowDetail(html);
        }
    });
}

function ShowDetail(content) {
    $(".detail-view").html(content);
    $(".detail-view").show();
    $(".gridview").css("display", "none");

}

function CloseDetail() {
    $(".detail-view").html('');
    $(".detail-view").hide();
    $(".gridview").css("display", "block");
}

function SubmitForm(form) {
    $(form).submit();
}

function Search() {
    var form = $(".search-form");
    $.ajax({
        url: $(form).attr('action'),
        type: 'post',
        data: $(form).serialize(),
        dataType: "html",
        beforeSend: function () {
            $(".se-pre-con").show();
        },
        error: function (jqXHR, textStatus, errorThrown) {
        },
        success: function (data) {
            $(".gridview").html(data);
        },
        complete: function () {
            $(".se-pre-con").hide();
        }
    });
    return false;
}

function ToggleComponent(chb, component) {
    $(component).attr('readonly', !$(chb).prop('checked'));
}

function ToggleComponent2(chb, component) {
    $(component).attr('readonly', $(chb).prop('checked'));
}

function HandleKeyPress(e) {
    var key = e.keyCode || e.which;
    if (key == 13) {
        e.preventDefault();
    }
}

function ChangeCategory(ddl) {
    if ($(ddl).val() != '')
        SelectingCateID = $(ddl).val();
    else
        SelectingCateID = "0";
}


function AddOptionForDDLSeason(ddl, lstSeason) {
    $(ddl).empty();
    $(ddl).append($('<option/>', {
        value: "",
        text: "-- Select --",
    }));

    for (var i = 0; i < lstSeason.length; i++) {
        $(ddl).append($('<option/>', {
            value: lstSeason[i].ID,
            text: lstSeason[i].Name + " [" + lstSeason[i].StoreName + "]"
        }));
    }
}

function SetSelectedSeason(ddl) {
    var currentSeasonID = $(ddl).parents('.form-group').find('input[name="SelectingSeason"]').val();
    if (currentSeasonID != "")
        $(ddl).find('option[value="' + currentSeasonID + '"]').attr("selected", "selected");
}

function ChangeSeason(ddl) {
    if ($(ddl).val() != "")
        $(ddl).parents('.form-group').find('input[name="SelectingSeason"]').val($(ddl).val());
}



function formatDate(date) {
    var hours = date.getHours();
    var minutes = date.getMinutes();
    //var ampm = hours >= 12 ? 'pm' : 'am';
    //hours = hours % 12;
    //hours = hours ? hours : 12; // the hour '0' should be '12'
    hours = hours < 10 ? '0' + hours : hours;
    minutes = minutes < 10 ? '0' + minutes : minutes;
    var strTime = hours + ':' + minutes;// + ' ' + ampm;

    var month = (date.getMonth() + 1) < 10 ? '0' + (date.getMonth() + 1) : (date.getMonth() + 1);
    var day = date.getDate() < 10 ? '0' + date.getDate() : date.getDate();
    var strDate = day + "/" + month + "/" + date.getFullYear();

    //return date.getMonth() + 1 + "/" + date.getDate() + "/" + date.getFullYear() + " " + strTime;
    return strDate + " " + strTime;
}

function disableButton(btn, status) {
    if (status) {
        $(btn).addClass('disabled');
    } else {
        $(btn).removeClass('disabled');
    }
}