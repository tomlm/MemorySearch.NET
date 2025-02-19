using Newtonsoft.Json;

namespace ObjectSearch.Net.Tests
{
    public class RecordItemBase
    {
        [JsonProperty("title")]
        public string Title { get; set; } = String.Empty;
    }

    public class RecordItem : RecordItemBase
    {
        [JsonProperty("description")]
        public string Description { get; set; } = String.Empty;
    }

    [TestClass]
    public class ObjectSearchEngineTests
    {
        [TestMethod]
        public void TestIndexStrings()
        {
            var searchEngine = new ObjectSearchEngine();
            searchEngine.AddObjects(Sentences);

            var results = searchEngine.Search<string>("french");
            var result = results.First();
            Assert.AreEqual("Mastering the art of French cooking with step-by-step recipes.", result.Value);
        }

        [TestMethod]
        public void TestIndexObjects()
        {
            var searchEngine = new ObjectSearchEngine()
                .AddObjects(Records);

            var results = searchEngine.Search<RecordItemBase>("entrepreneurship");
            Assert.AreEqual(Records.Single(r => r.Title.Contains("Entrepreneurship")), results.First().Value);

            var results2 = searchEngine.Search<RecordItem>("vegan", 1);
            Assert.AreEqual(Records.Single(r => r.Title.Contains("Vegan")), results2.First().Value);

            var results3 = searchEngine.Search<RecordItem>("insights", 1);
            Assert.AreEqual(Records.Single(r => r.Description.Contains("insights")), results3.First().Value);

            Assert.AreEqual(results.SearchEngine, results2.SearchEngine);
            Assert.AreEqual(results.SearchEngine, results3.SearchEngine);
        }

        [TestMethod]
        public void TestIndexMixedObjects()
        {
            var searchEngine = new ObjectSearchEngine();
            searchEngine.AddObjects(Sentences);
            searchEngine.AddObjects(Records);

            var results = searchEngine.Search<RecordItem>("entrepreneurship");
            Assert.AreEqual(Records.Single(r => r.Title.Contains("Entrepreneurship")), results.First().Value);

            var results2 = searchEngine.Search<string>("french", 1);
            var result2 = results2.First();
            Assert.AreEqual("Mastering the art of French cooking with step-by-step recipes.", result2.Value);
        }

        [TestMethod]
        public void TestSearchEngineAccess()
        {
            var searchEngine = new ObjectSearchEngine();
            searchEngine.AddObjects(Sentences);
            searchEngine.AddObjects(Records);

            var results = searchEngine.Search<RecordItem>("entrepreneurship");
            var results2 = searchEngine.Search<string>("french", 1);
            Assert.AreEqual(searchEngine, results.SearchEngine);
            Assert.AreEqual(searchEngine, results2.SearchEngine);
            foreach (var result in results)
                Assert.AreEqual(searchEngine, result.SearchEngine);
            foreach (var result in results2)
                Assert.AreEqual(searchEngine, result.SearchEngine);
        }


        [TestMethod]
        public void TestDeleteDocuments()
        {
            var searchEngine = new ObjectSearchEngine()
                .AddObjects(Sentences);

            var results = searchEngine.Search<string>("explor*");
            Assert.AreEqual(13, results.Count);

            searchEngine.RemoveObjects(results.Select(s => s.Value));
            results = searchEngine.Search<string>("explor*");
            Assert.AreEqual(0, results.Count);
        }

        [TestMethod]
        public void TestUpdateDocuments()
        {
            var searchEngine = new ObjectSearchEngine() 
                .AddObjects(Records);

            var results = searchEngine.Search<RecordItem>("entrepreneurship");
            Assert.AreEqual(1, results.Count);
            results[0].Value.Title = "gronk";
            searchEngine.UpdateObjects(new[] { results[0].Value });

            var results2 = searchEngine.Search<RecordItem>("gronk");
            Assert.AreEqual(results[0].Value, results2[0].Value);
            Assert.AreEqual("gronk", results[0].Value.Title);
            Assert.AreEqual("gronk", results2[0].Value.Title);

        }

        private static List<string> Sentences = new List<string>()
        {
            "The ultimate guide to growing organic vegetables in your backyard.",
            "Explore the world of abstract art through vibrant paintings.",
            "Discover the best hiking trails in the Pacific Northwest.",
            "A comprehensive history of classical music from Bach to Beethoven.",
            "Learn the secrets to perfect homemade sourdough bread.",
            "Top 10 destinations for a family-friendly vacation.",
            "The anatomy of a blockbuster movie: key elements of success.",
            "Understanding blockchain technology and its impact on industries.",
            "A beginner's guide to meditation and mindfulness techniques.",
            "Exploring ancient civilizations: an interactive timeline.",
            "The complete works of Shakespeare in digital format.",
            "How to create stunning digital illustrations: a tutorial.",
            "Essentials of entrepreneurship: launching your first startup.",
            "The science of climate change: facts and solutions.",
            "A curated list of the best mystery novels of the decade.",
            "Mastering the art of French cooking with step-by-step recipes.",
            "Introduction to astronomy: stargazing for beginners.",
            "The evolution of video games: from Pong to VR.",
            "Best practices for personal finance and budgeting.",
            "A deep dive into the psychology of color in design.",
            "The rise and fall of ancient Rome: an immersive experience.",
            "How to build a successful online business in 2023.",
            "The impact of artificial intelligence on everyday life.",
            "Gardening tips for small urban spaces and apartments.",
            "Famous speeches that changed the world: audio collection.",
            "Understanding the basics of SQL for data management.",
            "A walk through the iconic fashion trends of the 20th century.",
            "Exploring the wonders of the Great Barrier Reef.",
            "A parent's guide to navigating online education.",
            "The art of negotiation: strategies for success.",
            "Experience the magic of Broadway through iconic musicals.",
            "A guide to sustainable and eco-friendly living.",
            "Exploring the future of transportation: electric and autonomous vehicles.",
            "Learn to play the piano in 30 days: a beginner's course.",
            "The digital nomad lifestyle: working remotely from anywhere.",
            "A visual journey through national parks in the United States.",
            "Understanding mental health: important topics and resources.",
            "An introduction to the world of cryptocurrency investing.",
            "Discovering hidden gems in Paris: a local's guide.",
            "The evolution and significance of the Olympic Games.",
            "Creative writing prompts to spark your imagination.",
            "The best of Italian cuisine: regional recipes and traditions.",
            "A timeline of technological innovations in the 21st century.",
            "Exploring the philosophy of minimalism and simple living.",
            "An in-depth analysis of major world religions and beliefs.",
            "The essentials of effective communication in the workplace.",
            "How to photograph wildlife: tips and techniques.",
            "A beginner's guide to the stock market and investing.",
            "Exploring the cultural significance of tea in different countries.",
            "How to organize your home for maximum efficiency and comfort.",
            "The evolution of fashion photography: key figures and styles.",
            "Best practices for achieving work-life balance.",
            "Interactive map of historical landmarks around the world.",
            "How to host the perfect dinner party: etiquette and recipes.",
            "Understanding human rights: key principles and history.",
            "Creating a capsule wardrobe: style that lasts.",
            "An exploration of jazz music and its pioneers.",
            "The impact of the internet on modern communication.",
            "How to train for a marathon: essential tips and schedules.",
            "Understanding the fundamentals of digital marketing.",
            "Exploring urban architecture: famous cityscapes and skylines.",
            "The history and evolution of the English language.",
            "A guide to the most common bird species in North America.",
            "The art of storytelling in film and television.",
            "A practical approach to learning a new language.",
            "The rise of eSports: the future of competitive gaming.",
            "Crafting the perfect resume: tips and templates.",
            "Exploring the magic of Disney parks around the world.",
            "An overview of renewable energy sources and innovations.",
            "How to build emotional intelligence: tools and techniques.",
            "Managing stress in a fast-paced world: effective strategies.",
            "The best road trips to take in the United States.",
            "Exploring the influence of African culture on global music.",
            "A visual guide to famous architectural styles.",
            "The science behind effective habit formation and change.",
            "Understanding biodiversity and conservation efforts.",
            "Exploring the world's oldest libraries and archives.",
            "A culinary journey through spices and flavors of Asia.",
            "The sociology of social media: effects on society and identity.",
            "How to choose the perfect pet for your lifestyle.",
            "The history of animation: from hand-drawn to CGI.",
            "A beginner's guide to brewing your own beer.",
            "The art and science of successful goal setting.",
            "Discovering the world's most beautiful botanical gardens.",
            "The role of women in science through history.",
            "How to start a podcast: essential steps and equipment.",
            "Exploring the diversity of life on Earth: a biodiversity atlas.",
            "The ins and outs of classic car restoration.",
            "An intimate look at the works of famous poets.",
            "Navigating the college application process: a step-by-step guide.",
            "A celebration of LGBTQ+ history and culture worldwide.",
            "How to implement an effective exercise routine at home.",
            "A study of the greatest engineering feats in history.",
            "The relationship between diet and mental health.",
            "A chronological catalog of ancient scripts and alphabets.",
            "Exploring the history and influence of the Renaissance."
        };

#pragma warning disable CS8601 // Possible null reference assignment.
        private static List<RecordItem> Records = JsonConvert.DeserializeObject<List<RecordItem>>(
            $$"""
            [
                {
                    "title": "The Art of Coding",
                    "description": "A comprehensive guide to becoming a proficient programmer, covering various languages and techniques."
                },
                {
                    "title": "Mystical Mountains: A Travel Diary",
                    "description": "Explore the breathtaking landscapes and cultures of the world's most famous mountain ranges."
                },
                {
                    "title": "The Vegan Kitchen Revolution",
                    "description": "Discover healthy and delicious plant-based recipes that will transform your culinary experience."
                },
                {
                    "title": "Mastering Yoga and Mindfulness",
                    "description": "An insightful book on enhancing mental and physical well-being through yoga and mindfulness practices."
                },
                {
                    "title": "History's Greatest Empires",
                    "description": "Dive deep into the rise and fall of history's most influential empires across the globe."
                },
                {
                    "title": "Innovations in Renewable Energy",
                    "description": "An exploration of cutting-edge technologies and methods transforming the future of energy production."
                },
                {
                    "title": "The Ultimate Guide to Digital Marketing",
                    "description": "Essential strategies and tools for creating effective digital marketing campaigns in the modern world."
                },
                {
                    "title": "Fantasy Worlds Unleashed",
                    "description": "A captivating collection of world-building tips, character development, and storytelling techniques for fantasy fiction writers."
                },
                {
                    "title": "Living a Minimalist Life",
                    "description": "Practical advice for achieving simplicity and focusing on what truly matters in life through minimalism."
                },
                {
                    "title": "Photography Essentials for Beginners",
                    "description": "A step-by-step guide to understanding the basics of photography, from composition to post-processing."
                },
                {
                    "title": "Meditation for Busy Minds",
                    "description": "Strategies for integrating meditation practices into hectic lifestyles for improved mental clarity and peace."
                },
                {
                    "title": "The Brain's Potential: Unlock Your Genius",
                    "description": "Insights and techniques to enhance cognitive skills and maximize brain performance in everyday activities."
                },
                {
                    "title": "Seas of Tranquility",
                    "description": "A journey through the world's most serene and beautiful coastal destinations, perfect for relaxation and adventure."
                },
                {
                    "title": "Cryptocurrency and the Future of Finance",
                    "description": "An examination of digital currencies and blockchain technology, analyzing their impact on the global financial system."
                },
                {
                    "title": "Gardening in Small Spaces",
                    "description": "Creative solutions and tips for cultivating a thriving garden in limited spaces such as balconies and patios."
                },
                {
                    "title": "The Language of Flowers",
                    "description": "An exploration of the symbolic meanings behind flowers and their impact on art, culture, and personal expression."
                },
                {
                    "title": "Urban Wildlife: A Practical Field Guide",
                    "description": "A guide to identifying and understanding the diverse wildlife that thrives in urban environments."
                },
                {
                    "title": "Astronomy for Stargazers",
                    "description": "A beginner's guide to observing celestial phenomena and understanding the wonders of our night sky."
                },
                {
                    "title": "Entrepreneurship in the Modern Era",
                    "description": "Essential insights into starting and growing successful businesses in today's dynamic global economy."
                },
                {
                    "title": "The Enchanted Forest: A Children's Fairy Tale",
                    "description": "A magical storybook filled with enchanting tales and adventures for young readers to explore fantasy worlds."
                }
            ]
            """);
#pragma warning restore CS8601 // Possible null reference assignment.

    }
}