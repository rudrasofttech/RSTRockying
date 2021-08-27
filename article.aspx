﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeFile="article.aspx.cs" Inherits="_Article" %>

<%@ Import Namespace="Rockying.Models" %>

<asp:Content ID="conten3" ContentPlaceHolderID="TopContent" runat="server">
    <title><%: string.IsNullOrEmpty(PPM.Item.MetaTitle) ? PPM.Item.Title : PPM.Item.MetaTitle %></title>
    <meta name="description" content="<%: PPM.Item.OGDescription %>" />
    <meta name="keywords" content="<%: PPM.Item.Tag %>" />
    <meta name="author" content="<%: PPM.Item.WriterName %>" />
    <meta property="og:type" content="article" />
    <meta property="og:url" content="https://<%: Request.Url.Host %>/a/<%: PPM.Item.URL.ToString() %>" />
    <meta property="og:site_name" content="<%: Utility.SiteName %>" />
    <meta property="fb:admins" content="871950590" />
    <meta property="og:title" content="<%: string.IsNullOrEmpty(PPM.Item.MetaTitle) ? PPM.Item.Title : PPM.Item.MetaTitle %>" />
    <meta property="og:description" content="<%: PPM.Item.OGDescription %>" />
    <meta property="og:image" content="<%: PPM.Item.OGImage %>" />

</asp:Content>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">

    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/4.7.0/css/font-awesome.min.css">
    <style>
        .checked {
            color: orange;
        }
    </style>
    <script src="//code.jquery.com/ui/1.10.3/jquery-ui.js" type="text/javascript"></script>
    <%if (PPM.Item.Text != null && PPM.Item.Text.Contains("rstquestion"))
        { %>
    <link href="//www.rockying.com/bootstrap/css/customquestion.css" rel="stylesheet"
        type="text/css" />
    <link href="//www.rudrasofttech.com/js-tools/question/question.css" rel="stylesheet"
        type="text/css" />
    <script src="//<%: Request.Url.Host %>/bootstrap/js/rstquestion.js" type="text/javascript"></script>
    <script src="//www.rudrasofttech.com/js-tools/question/question.js" type="text/javascript"></script>
    <script type="text/javascript">
        $(window).load(function () {
            $(".rstquestion").question({ Attempts: 3, Effect: false });
        });
    </script>
    <%} %>
    <%if (PPM.Item.Text != null && PPM.Item.Text.Contains("slideshow"))
        { %>
    <link href="//www.rudrasofttech.com/js-tools/slideshow/slides.css" rel="stylesheet"
        type="text/css" />
    <script src="//www.rudrasofttech.com/js-tools/slideshow/slides.js" type="text/javascript"></script>
    <script src="//www.rudrasofttech.com/js-tools/swipe/swipe.js" type="text/javascript"></script>
    <script type="text/javascript">
        $(window).load(function () {
            $(".slideshow").slides({ ItemSelector: ".slide", ScrollInterval: 60000, Circular: true, HoverPause: true, NextHandle: $("#nextslide"), PrevHandle: $("#prevslide") });
        });
    </script>
    <style type="text/css">
        .slideshow {
            width: 100%;
            background-color: #fff;
            overflow: hidden;
        }
    </style>
    <%} %>
    <% if (!string.IsNullOrEmpty(PPM.AudioURL))
        { %>
    <script src="//www.rockying.com/bootstrap/js/htmlslider.js" type="text/javascript"></script>
    <%} %>
    <script type="text/javascript">
        $(window).load(function () {
            $(".youtube").modalbox({
                Type: 'iframe',
                Width: 560,
                Height: 315,
                Modal: true
            });

        });
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <div class="row-fluid">
        <div class="span12" style="text-align: center;">
            <h1>
                <%: PPM.Item.Title%>
            </h1>
            <p class="smalltxt" style="color: #333333;">
                Written By <%: PPM.Item.WriterName%>, 
                <%if (PPM.ArticleCategory == null)
                    { %>
                <%: PPM.Item.CategoryName%> Story
                <%}
                    else
                    { %><a href="<%= string.Format("../{0}/index", PPM.ArticleCategory.UrlName) %>"><%: PPM.Item.CategoryName%> Story</a>
                <%} %>
                <%if (PostRating > 0 && PostRating <= 1)
                    { %>
                <span class="fa fa-star checked"></span>
                <span class="fa fa-star"></span>
                <span class="fa fa-star"></span>
                <span class="fa fa-star"></span>
                <span class="fa fa-star"></span>
                <%}
                    else if (PostRating > 1 && PostRating <= 2)
                    {%>
                <span class="fa fa-star checked"></span>
                <span class="fa fa-star checked"></span>
                <span class="fa fa-star"></span>
                <span class="fa fa-star"></span>
                <span class="fa fa-star"></span>
                <%}
                    else if (PostRating > 2 && PostRating <= 3)
                    {%>
                <span class="fa fa-star checked"></span>
                <span class="fa fa-star checked"></span>
                <span class="fa fa-star checked"></span>
                <span class="fa fa-star"></span>
                <span class="fa fa-star"></span>
                <%}
                    else if (PostRating > 3 && PostRating <= 4)
                    {%>
                <span class="fa fa-star checked"></span>
                <span class="fa fa-star checked"></span>
                <span class="fa fa-star checked"></span>
                <span class="fa fa-star checked"></span>
                <span class="fa fa-star"></span>
                <%}
                    else if (PostRating > 4 && PostRating <= 5)
                    {%>
                <span class="fa fa-star checked"></span>
                <span class="fa fa-star checked"></span>
                <span class="fa fa-star checked"></span>
                <span class="fa fa-star checked"></span>
                <span class="fa fa-star checked"></span>
                <%} %>
            </p>
            <%
                //articles created by admin have their own images.
                if (PPM.ArticleCreator.UserType != (byte)MemberTypeType.Admin)
                { %>
            <div style="text-align: center;">
                <img src="<%: PPM.Item.OGImage %>" class="img-rounded" alt="" style="margin-top: 5px; margin-bottom: 10px;" />
            </div>
            <%} %>
            <% 
                //this html will only be added if there is an audio avialable
                if (!string.IsNullOrEmpty(PPM.AudioURL))
                { %>
            <div style="padding: 5px; background: #f8f9fa; text-align: center; max-width: 80%; border-radius: 10px; position: fixed; bottom: 2px; width: 300px; left: 50%; margin-left: -150px;">
                <h4>You can listen to this story</h4>
                <table style="width: 100%;">
                    <tbody>
                        <tr>
                            <td style="width: 30px;">
                                <button class="btn btn-danger show" type="button" id="btnPlay" onclick="playStoryAudio();"><i class="icon-play icon-white"></i></button>
                                <button class="btn btn-danger hide" id="btnPause" type="button" onclick="pauseStoryAudio();"><i class="icon-pause icon-white"></i></button>
                            </td>
                            <td style="width: 30px;"><span id="currenttime" class="label">0</span></td>
                            <td>
                                <input type="range" id="rngSeek" min="0" value="0" onchange="changeSeek(this.value)" /></td>
                            <td style="width: 30px;"><span id="totaltime" class="label"></span></td>
                        </tr>
                    </tbody>
                </table>
            </div>
            <script>
                var saudio = new Audio('<%= PPM.AudioURL %>');
                saudio.onloadedmetadata = function () {
                    $('#totaltime').html(parseInt(saudio.duration, 10));
                }
                saudio.ontimeupdate = function () {
                    $('#rngSeek').val(saudio.currentTime);
                    $('#currenttime').html(parseInt(saudio.currentTime, 10));
                }
                saudio.onplay = function () {
                    $("#btnPlay").removeClass("show").addClass("hide");
                    $("#btnPause").removeClass("hide").addClass("show").focus();
                    $('#rngSeek').attr('max', saudio.duration);
                    $('#totaltime').html(parseInt(saudio.duration, 10));

                }
                saudio.onpause = function () {
                    $("#btnPause").removeClass("show").addClass("hide");
                    $("#btnPlay").removeClass("hide").addClass("show").focus();
                }
                function changeSeek(val) {
                    saudio.currentTime = val;
                }
                function playStoryAudio() {
                    saudio.play();
                }
                function pauseStoryAudio() {
                    saudio.pause();
                }
            </script>
            <%} %>
            <div id="article">
                <%= PPM.Item.Text %>
            </div>
            <h4 style="margin-top: 20px;">
                <span class="sectionheading">Rate The Story</span>
            </h4>
            <div style="margin: 10px 0px; text-align: center;">
                <div class="btn-toolbar" style="margin: 0;">
                    <div class="btn-group">
                        <button type="button" id="btn1star" class="btn btn-large" onclick="postRating(1)" onmouseover="toggleCheckedStar(1, true)" onmouseout="toggleCheckedStar(1, false)">1 <span class="fa fa-star"></span></button>
                        <button type="button" id="btn2star" class="btn btn-large" onclick="postRating(2)" onmouseover="toggleCheckedStar(2, true)" onmouseout="toggleCheckedStar(2, false)">2 <span class="fa fa-star"></span></button>
                        <button type="button" id="btn3star" class="btn btn-large" onclick="postRating(3)" onmouseover="toggleCheckedStar(3, true)" onmouseout="toggleCheckedStar(3, false)">3 <span class="fa fa-star"></span></button>
                        <button type="button" id="btn4star" class="btn btn-large" onclick="postRating(4)" onmouseover="toggleCheckedStar(4, true)" onmouseout="toggleCheckedStar(4, false)">4 <span class="fa fa-star"></span></button>
                        <button type="button" id="btn5star" class="btn btn-large" onclick="postRating(5)" onmouseover="toggleCheckedStar(5, true)" onmouseout="toggleCheckedStar(5, false)">5 <span class="fa fa-star"></span></button>
                    </div>
                </div>
            </div>
            <h4 style="margin-top: 20px;">
                <span class="sectionheading">Share It</span>
            </h4>
            <div class="addthis_inline_share_toolbox"></div>
            <div style="margin: 10px 0px;">
                <iframe src="//www.rockying.com/account/subscribe" style="width: 100%; height: 300px; border: 0px;"></iframe>
            </div>
            <div id="disqus_thread"></div>
            <script type="text/javascript">

                /**
                *  RECOMMENDED CONFIGURATION VARIABLES: EDIT AND UNCOMMENT THE SECTION BELOW TO INSERT DYNAMIC VALUES FROM YOUR PLATFORM OR CMS.
                *  LEARN WHY DEFINING THESE VARIABLES IS IMPORTANT: https://disqus.com/admin/universalcode/#configuration-variables*/
                /*
                var disqus_config = function () {
                this.page.url = PAGE_URL;  // Replace PAGE_URL with your page's canonical URL variable
                this.page.identifier = PAGE_IDENTIFIER; // Replace PAGE_IDENTIFIER with your page's unique identifier variable
                };
                */
                (function () { // DON'T EDIT BELOW THIS LINE
                    var d = document, s = d.createElement('script');
                    s.src = 'https://rockying.disqus.com/embed.js';
                    s.setAttribute('data-timestamp', +new Date());
                    (d.head || d.body).appendChild(s);
                })();
            </script>
            <noscript>Please enable JavaScript to view the <a href="https://disqus.com/?ref_noscript">comments powered by Disqus.</a></noscript>

            <script type="text/javascript">
                /* * * CONFIGURATION VARIABLES: EDIT BEFORE PASTING INTO YOUR WEBPAGE * * */
                var disqus_shortname = 'rockying'; // required: replace example with your forum shortname

                /* * * DON'T EDIT BELOW THIS LINE * * */
                (function () {
                    var s = document.createElement('script'); s.async = true;
                    s.type = 'text/javascript';
                    s.src = '//' + disqus_shortname + '.disqus.com/count.js';
                    (document.getElementsByTagName('HEAD')[0] || document.getElementsByTagName('BODY')[0]).appendChild(s);
                }());
            </script>
        </div>
    </div>
    <%if (PPM.RecommendationList.Count > 0)
        { %>
    <div class="row-fluid" style="text-align: center;">
        <h6>
            <span class="sectionheading">Recommendations</span>
        </h6>
        <ul class="thumbnails">
            <%for (int i = 1; i <= PPM.RecommendationList.Count; i++)
                {
                    Article p = PPM.RecommendationList[i - 1];
            %>
            <li class="span4" style="max-height: 375px; overflow-y: hidden;">
                <div class="thumbnail">
                    <%if (!string.IsNullOrEmpty(p.OGImage))
                        { %>
                    <a href="//www.rockying.com/a/<%= p.URL %>">
                        <span class="articleimage" style="display: block; background-image: url(<%= p.OGImage %>)"></span></a>
                    <%} %>
                    <div class="caption">
                        <h3><a href="//www.rockying.com/a/<%= p.URL %>" style="">
                            <%: p.Title %>
                        </a></h3>
                        <p><%:p.OGDescription %> </p>
                    </div>
                </div>
            </li>
            <% if (i % 3 == 0)
                { %>
        </ul>
        <ul class="thumbnails">
            <%} %>
            <%} %>
        </ul>
    </div>
    <%} %>
    <% if (CurrentUser != null)
        {
            if (CurrentUser.UserType == (byte)MemberTypeType.Admin || CurrentUser.ID == PPM.Item.CreatedBy)
            {%>
    <div style="padding-left: 10px; padding-top: 5px; padding-bottom: 5px; padding-right: 10px; position: fixed; top: auto; bottom: 0px; left: 0px; right: auto; background-color: #fff;">
        <a href="//<%: Request.Url.Host%>/Admin/ManageArticle.aspx?id=<%: PPM.Item.ID.ToString()%>&mode=edit"
            target="_blank">Edit</a>
    </div>
    <% }
        }%>
    <script>
        if (sessionStorage.getItem("subscribeshown") == null) {
            setTimeout(function () { $("#subscribeBtn").click(); sessionStorage.setItem("subscribeshown", "true"); }, 5000);
        }

        function toggleCheckedStar(val, toggle) {
            for (i = 1; i <= val; i++) {
                if (toggle) {
                    $("#btn" + i + "star > span.fa.fa-star").addClass("checked");
                } else {
                    $("#btn" + i + "star > span.fa.fa-star").removeClass("checked");
                }
            }
        }

        function postRating(val) {
            $.post("//www.rockying.com/handlers/rating.ashx", { star: val, post: parseInt("<%= PPM.Item.ID.ToString()%>", 10), comment: '', ip: ip, visitid: sessionStorage.getItem("vid"), action: "add" },
                function () {
                });
        }
        
    </script>
</asp:Content>
