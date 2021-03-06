﻿namespace RssReader.Storage
{
    using System;
    using System.Collections.Generic;

    using Caliburn.Micro;

    using HtmlAgilityPack;

    using Windows.UI;
    using Windows.UI.Xaml;
    using Windows.UI.Xaml.Controls;
    using Windows.UI.Xaml.Documents;
    using Windows.UI.Xaml.Media;
    using Windows.UI.Xaml.Media.Imaging;
    using Windows.UI.Xaml.Shapes;

    public class HtmlToBlocksConverter : IHtmlToBlocksConverter
    {
        private IEventAggregator eventAggregator;

        public HtmlToBlocksConverter(IEventAggregator eventAggregator)
        {
            this.eventAggregator = eventAggregator;
        }

        public List<Block> GenerateBlocksForHtml(string xhtml)
        {
            var bc = new List<Block>();

            try
            {
                var doc = new HtmlDocument();
                doc.LoadHtml(xhtml);

                var b = GenerateParagraph(doc.DocumentNode);
                bc.Add(b);
            }
            catch (Exception e)
            {
                eventAggregator.Publish(e.Message, Execute.OnUIThread);
            }

            return bc;
        }

        private static string CleanText(string input)
        {
            var clean = Windows.Data.Html.HtmlUtilities.ConvertToText(input);
            if (clean == "\0")
                clean = "\n";
            return clean;
        }

        private static Block GenerateBlockForTopNode(HtmlNode node)
        {
            return GenerateParagraph(node);
        }

        private static void AddChildren(Paragraph p, HtmlNode node)
        {
            var added = false;
            foreach (var child in node.ChildNodes)
            {
                var i = GenerateBlockForNode(child);
                if (i != null)
                {
                    p.Inlines.Add(i);
                    added = true;
                }
            }
            if (!added)
            {
                p.Inlines.Add(new Run() { Text = CleanText(node.InnerText) });
            }
        }

        private static void AddChildren(Span s, HtmlNode node)
        {
            var added = false;

            foreach (var child in node.ChildNodes)
            {
                var i = GenerateBlockForNode(child);
                if (i != null)
                {
                    s.Inlines.Add(i);
                    added = true;
                }
            }
            if (!added)
            {
                s.Inlines.Add(new Run() { Text = CleanText(node.InnerText) });
            }
        }

        private static Inline GenerateBlockForNode(HtmlNode node)
        {
            switch (node.Name)
            {
                case "div":
                    return GenerateSpan(node);
                case "p":
                case "P":
                    return GenerateInnerParagraph(node);
                case "img":
                case "IMG":
                    return GenerateImage(node);
                case "a":
                case "A":
                    if (node.ChildNodes.Count >= 1 && (node.FirstChild.Name == "img" || node.FirstChild.Name == "IMG"))
                    {
                        return GenerateImage(node.FirstChild);
                    }
                    return GenerateHyperLink(node);
                case "li":
                case "LI":
                    return GenerateLI(node);
                case "b":
                case "B":
                    return GenerateBold(node);
                case "i":
                case "I":
                    return GenerateItalic(node);
                case "u":
                case "U":
                    return GenerateUnderline(node);
                case "br":
                case "BR":
                    return new LineBreak();
                case "span":
                case "Span":
                    return GenerateSpan(node);
                case "iframe":
                case "Iframe":
                    return GenerateIFrame(node);
                case "#text":
                    if (!string.IsNullOrWhiteSpace(node.InnerText))
                    {
                        return new Run() { Text = CleanText(node.InnerText) };
                    }
                    break;
                case "h1":
                case "H1":
                    return GenerateH1(node);
                case "h2":
                case "H2":
                    return GenerateH2(node);
                case "h3":
                case "H3":
                    return GenerateH3(node);
                case "ul":
                case "UL":
                    return GenerateUL(node);
                default:
                    return GenerateSpanWNewLine(node);
            }
            return null;
        }

        private static Inline GenerateLI(HtmlNode node)
        {
            var s = new Span();
            var iui = new InlineUIContainer();
            var ellipse = new Ellipse();
            ellipse.Fill = new SolidColorBrush(Colors.Black);
            ellipse.Width = 6;
            ellipse.Height = 6;
            ellipse.Margin = new Thickness(-30, 0, 0, 1);
            iui.Child = ellipse;
            s.Inlines.Add(iui);
            AddChildren(s, node);
            s.Inlines.Add(new LineBreak());
            return s;
        }

        private static Inline GenerateImage(HtmlNode node)
        {
            var s = new Span();

            var iui = new InlineUIContainer();
            var sourceUri = System.Net.WebUtility.HtmlDecode(node.Attributes["src"].Value);
            if (!sourceUri.StartsWith("http://")) sourceUri = "http:" + sourceUri;
            var img = new Image { Source = new BitmapImage(new Uri(sourceUri, UriKind.Absolute)) };
            img.Stretch = Stretch.Fill;
            img.UseLayoutRounding = true;
            img.VerticalAlignment = VerticalAlignment.Top;
            img.HorizontalAlignment = HorizontalAlignment.Left;
            img.ImageOpened += ImgImageOpened;
            img.MaxWidth = 300;
            img.MaxHeight = 200;
            iui.Child = img;
            s.Inlines.Add(iui);
            s.Inlines.Add(new LineBreak());

            return s;
        }

        private static void ImgImageOpened(object sender, RoutedEventArgs e)
        {
            var img = sender as Image;
            var bimg = img.Source as BitmapImage;
            if (bimg.PixelWidth > 800 || bimg.PixelHeight > 600)
            {
                img.Width = 800;
                img.Height = 600;
                if (bimg.PixelWidth > 800)
                {
                    img.Width = 800;
                    img.Height = (800.0 / (double)bimg.PixelWidth) * bimg.PixelHeight;
                }
                if (img.Height > 600)
                {
                    img.Height = 600;
                    img.Width = (600.0 / (double)img.Height) * img.Width;
                }
            }
            else
            {
                img.Height = bimg.PixelHeight;
                img.Width = bimg.PixelWidth;
            }
        }

        private static Inline GenerateHyperLink(HtmlNode node)
        {
            var s = new Span();
            var iui = new InlineUIContainer();
            var hb = new HyperlinkButton() { NavigateUri = new Uri(node.Attributes["href"].Value, UriKind.Absolute), Content = CleanText(node.InnerText) };
            hb.FontSize = 12;
            iui.Child = hb;
            s.Inlines.Add(iui);
            return s;
        }

        private static Inline GenerateIFrame(HtmlNode node)
        {
            try
            {
                var s = new Span();
                s.Inlines.Add(new LineBreak());
                var iui = new InlineUIContainer();
                var ww = new WebView() { Source = new Uri(node.Attributes["src"].Value, UriKind.Absolute), Width = int.Parse(node.Attributes["width"].Value), Height = int.Parse(node.Attributes["height"].Value) };
                iui.Child = ww;
                s.Inlines.Add(iui);
                s.Inlines.Add(new LineBreak());
                return s;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        private static Block GenerateTopIFrame(HtmlNode node)
        {
            try
            {
                var p = new Paragraph();
                var iui = new InlineUIContainer();
                var ww = new WebView() { Source = new Uri(node.Attributes["src"].Value, UriKind.Absolute), Width = int.Parse(node.Attributes["width"].Value), Height = int.Parse(node.Attributes["height"].Value) };
                iui.Child = ww;
                p.Inlines.Add(iui);
                return p;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        private static Inline GenerateBold(HtmlNode node)
        {
            var b = new Bold();
            AddChildren(b, node);
            return b;
        }

        private static Inline GenerateUnderline(HtmlNode node)
        {
            var u = new Underline();
            AddChildren(u, node);
            return u;
        }

        private static Inline GenerateItalic(HtmlNode node)
        {
            var i = new Italic();
            AddChildren(i, node);
            return i;
        }

        private static Block GenerateParagraph(HtmlNode node)
        {
            var p = new Paragraph();
            AddChildren(p, node);
            return p;
        }

        private static Inline GenerateUL(HtmlNode node)
        {
            var s = new Span();
            s.Inlines.Add(new LineBreak());
            AddChildren(s, node);
            return s;
        }

        private static Inline GenerateInnerParagraph(HtmlNode node)
        {
            var s = new Span();
            s.Inlines.Add(new LineBreak());
            AddChildren(s, node);
            s.Inlines.Add(new LineBreak());
            return s;
        }

        private static Inline GenerateSpan(HtmlNode node)
        {
            var s = new Span();
            AddChildren(s, node);
            return s;
        }

        private static Inline GenerateSpanWNewLine(HtmlNode node)
        {
            var s = new Span();
            AddChildren(s, node);
            if (s.Inlines.Count > 0)
                s.Inlines.Add(new LineBreak());
            return s;
        }

        private static Span GenerateH3(HtmlNode node)
        {
            var s = new Span();
            s.Inlines.Add(new LineBreak());
            var bold = new Bold();
            var r = new Run() { Text = CleanText(node.InnerText) };
            bold.Inlines.Add(r);
            s.Inlines.Add(bold);
            s.Inlines.Add(new LineBreak());
            return s;
        }

        private static Inline GenerateH2(HtmlNode node)
        {
            var s = new Span() { FontSize = 24 };
            s.Inlines.Add(new LineBreak());
            var r = new Run() { Text = CleanText(node.InnerText) };
            s.Inlines.Add(r);
            s.Inlines.Add(new LineBreak());
            return s;
        }

        private static Inline GenerateH1(HtmlNode node)
        {
            var s = new Span() { FontSize = 30 };
            s.Inlines.Add(new LineBreak());
            var r = new Run() { Text = CleanText(node.InnerText) };
            s.Inlines.Add(r);
            s.Inlines.Add(new LineBreak());
            return s;
        }
    }
}
