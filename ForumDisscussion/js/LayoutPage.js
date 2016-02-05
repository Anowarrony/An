   function ShowBusyWindow() {

                $("#DialgRef").fadeIn('slow');
                $("#dialog").fadeIn('slow');
            }
            $("#ProfileLink,#UnasweredQueslink,#AsweredQueslink,#QuestionPostlink,#HomePagelink,#Logoutlink,#LoginLink,#HelpLink").click(function () {
            
                ShowBusyWindow();
            });
            $(function () {
                $(".btn").css('color', 'black');
            });
       
            $(".search").css('width', '300px');
            $("#searchButton").css('color', 'black');
                
            var tooltip = $("#serbox").kendoTooltip({
                filter: "a",
                width: 80,
                height: 130,
                position: "top"
            }).data("kendoTooltip");


            $(".search").kendoAutoComplete({
                minLength: 2,



                placeholder: 'Search By Category',
                dataSource: {
                    type: "JSON",

                    transport: {
                        read: "/Home/DisplayCat"

                    },
                }

            });

