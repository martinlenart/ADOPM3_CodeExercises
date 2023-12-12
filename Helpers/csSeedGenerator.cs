using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Helpers
{
    #region exported types
    public interface ISeed<T>
    {
        //In order to separate from real and seeded instances
        public bool Seeded { get; set; }

        //Seeded The instance
        public T Seed(csSeedGenerator seedGenerator);
    }

    public class csSeededLatin
    {
        public string Paragraph { get; init; }
        public List<string> Sentences { get; init; }
        public List<string> Words { get; init; }
    }

    public class csSeededQuote
    {
        public string Quote { get; init; }
        public string Author { get; init; }
    }
    #endregion

    public class csSeedGenerator : Random
    {
        csSeedJsonContent _seeds = null;

        #region Names
        public string PetName => _seeds._names.PetNames[this.Next(0, _seeds._names.PetNames.Count)];
        public string FirstName => _seeds._names.FirstNames[this.Next(0, _seeds._names.FirstNames.Count)];
        public string LastName => _seeds._names.LastNames[this.Next(0, _seeds._names.LastNames.Count)];
        public string FullName => $"{FirstName} {LastName}";
        #endregion

        #region Addresses
        public string Country => _seeds._addresses[this.Next(0, _seeds._addresses.Count)].Country;
        public string City(string Country = null)
        {
            if (Country != null)
            {
                var adr = _seeds._addresses.FirstOrDefault(c => c.Country.ToLower() == Country.Trim().ToLower());
                if (adr == null)
                    throw new Exception("Country not found");

                return adr.Cities[this.Next(0, adr.Cities.Count)];
            }

            var tmp = _seeds._addresses[this.Next(0, _seeds._addresses.Count)];
            return tmp.Cities[this.Next(0, tmp.Cities.Count)];
        }
        public string StreetAddress(string Country = null)
        {
            if (Country != null)
            {
                var adr = _seeds._addresses.FirstOrDefault(c => c.Country.ToLower() == Country.Trim().ToLower());
                if (adr == null)
                    throw new Exception("Country not found");

                return $"{adr.Streets[this.Next(0, adr.Streets.Count)]} {this.Next(1, 100)}";
            }

            var tmp = _seeds._addresses[this.Next(0, _seeds._addresses.Count)];
            return $"{tmp.Streets[this.Next(0, tmp.Streets.Count)]} {this.Next(1, 100)}";
        }
        public int ZipCode => this.Next(10101, 100000);
        #endregion

        #region Emails and phones
        public string Email(string fname = null, string lname = null)
        {
            fname ??= FirstName;
            lname ??= LastName;

            return $"{fname}.{lname}@{_seeds._domains.Domains[this.Next(0, _seeds._domains.Domains.Count)]}";
        }

        public string PhoneNr => $"{this.Next(700, 800)} {this.Next(100, 1000)} {this.Next(100, 1000)}";
        #endregion

        #region Quotes
        public List<csSeededQuote> AllQuotes => _seeds._quotes
            .Select(q => new csSeededQuote { Quote = q.Quote, Author = q.Author })
            .ToList<csSeededQuote>();

        public List<csSeededQuote> Quotes(int tryNrOfItems)
        {
            return UniqueIndexPickFromList(tryNrOfItems, AllQuotes);
        }

        public csSeededQuote Quote => Quotes(1).FirstOrDefault();

        #endregion

        #region Latin
        public List<csSeededLatin> AllLatin => _seeds._latin
            .Select(l => new csSeededLatin { Paragraph = l.Paragraph, Sentences = l.Sentences, Words = l.Words })
            .ToList();

        public List<csSeededLatin> LatinParagraphs(int tryNrOfItems)
        {
            return UniqueIndexPickFromList(tryNrOfItems, AllLatin);
        }

        public List<string> LatinSentences(int tryNrOfItems)
        {
            var sRet = new List<string>();
            for (int i = 0; i < tryNrOfItems; i++)
            {
                var pIdx = this.Next(0, AllLatin.Count);
                var sIdx = this.Next(0, AllLatin[pIdx].Sentences.Count);

                sRet.Add(AllLatin[pIdx].Sentences[sIdx]);
            }
            return sRet;
        }

        public List<string> LatinWords(int tryNrOfItems)
        {
            var sRet = new List<string>();
            for (int i = 0; i < tryNrOfItems; i++)
            {
                var pIdx = this.Next(0, AllLatin.Count);
                var wIdx = this.Next(0, AllLatin[pIdx].Words.Count);

                sRet.Add(AllLatin[pIdx].Words[wIdx]);
            }
            return sRet;
        }

        public string LatinParagraph => LatinParagraphs(1).FirstOrDefault().Paragraph;
        public string LatinSentence => LatinSentences(1).FirstOrDefault();
        #endregion

        #region Music
        public string MusicBandName => "The " + _seeds._music.GroupNames[this.Next(0, _seeds._music.GroupNames.Count)]
            + " " + _seeds._music.GroupNames[this.Next(0, _seeds._music.GroupNames.Count)];

        public string MusicAlbumName => _seeds._music.AlbumPrefix[this.Next(0, _seeds._music.AlbumPrefix.Count)]
            + " " + _seeds._music.AlbumNames[this.Next(0, _seeds._music.AlbumNames.Count)]
            + " " + _seeds._music.AlbumNames[this.Next(0, _seeds._music.AlbumNames.Count)]
            + " " + _seeds._music.AlbumSuffix[this.Next(0, _seeds._music.AlbumSuffix.Count)];
        #endregion

        #region DateTime, bool and decimal
        public DateTime DateAndTime(int? fromYear = null, int? toYear = null)
        {
            bool dateOK = false;
            DateTime _date = default;
            while (!dateOK)
            {
                fromYear ??= DateTime.Today.Year;
                toYear ??= DateTime.Today.Year + 1;

                try
                {
                    int year = this.Next(Math.Min(fromYear.Value, toYear.Value),
                        Math.Max(fromYear.Value, toYear.Value));
                    int month = this.Next(1, 13);
                    int day = this.Next(1, 32);

                    _date = new DateTime(year, month, day);
                    dateOK = true;
                }
                catch
                {
                    dateOK = false;
                }
            }

            return DateTime.SpecifyKind(_date, DateTimeKind.Utc);
        }

        public bool Bool => (this.Next(0, 10) < 5) ? true : false;

        public decimal NextDecimal(int _from, int _to) => this.Next(_from * 1000, _to * 1000) / 1000M;
        #endregion

        #region From own String, Enum and List<TItem>
        public string FromString(string _inputString, string _splitDelimiter = ", ")
        {
            var _sarray = _inputString.Split(_splitDelimiter);
            return _sarray[this.Next(0, _sarray.Length)];
        }
        public TEnum FromEnum<TEnum>() where TEnum : struct
        {
            if (typeof(TEnum).IsEnum)
            {

                var _names = typeof(TEnum).GetEnumNames();
                var _name = _names[this.Next(0, _names.Length)];

                return Enum.Parse<TEnum>(_name);
            }
            throw new ArgumentException("Not an enum type");
        }
        public TItem FromList<TItem>(List<TItem> items)
        {
            return items[this.Next(0, items.Count)];
        }
        #endregion

        #region Generate seeded List of TItem

        //ISeed<TItem> has to be implemented to use this method
        public List<TItem> ToList<TItem>(int NrOfItems)
            where TItem : ISeed<TItem>, new()
        {
            //Create a list of seeded items
            var _list = new List<TItem>();
            for (int c = 0; c < NrOfItems; c++)
            {
                _list.Add(new TItem() { Seeded = true }.Seed(this));
            }
            return _list;
        }

        //Create a list of unique randomly seeded items
        public List<TItem> UniqueItemToList<TItem>(int tryNrOfItems, List<TItem> appendToUnique = null)
            where TItem : ISeed<TItem>, IEquatable<TItem>, new()
        {
            //Create a list of uniquely seeded items
            HashSet<TItem> _set = (appendToUnique == null) ? new HashSet<TItem>() : new HashSet<TItem>(appendToUnique);

            while (_set.Count < tryNrOfItems)
            {
                var _item = new TItem() { Seeded = true }.Seed(this);

                int _preCount = _set.Count();
                int tries = 0;
                do
                {
                    _set.Add(_item);

                    if (_set.Count == _preCount)
                    {
                        //Item was already in the _set. Generate a new one
                        _item = new TItem() { Seeded = true }.Seed(this);
                        ++tries;

                        //Does not seem to be able to generate new unique item
                        if (tries > 5)
                            return _set.ToList();
                    }

                } while (_set.Count <= _preCount);
            }

            return _set.ToList();
        }

        //Pick a number of unique items from a list of TItem (the List does not have to be unique)
        //IEquatable<TItem> has to be implemented to use this method
        public List<TItem> UniqueItemPickFromList<TItem>(int tryNrOfItems, List<TItem> list)
        where TItem : IEquatable<TItem>
        {
            //Create a list of uniquely seeded items
            HashSet<TItem> _set = new HashSet<TItem>();

            while (_set.Count < tryNrOfItems)
            {
                var _item = list[this.Next(0, list.Count)];

                int _preCount = _set.Count();
                int tries = 0;
                do
                {
                    _set.Add(_item);

                    if (_set.Count == _preCount)
                    {
                        //Item was already in the _set. Pick a new one
                        _item = list[this.Next(0, list.Count)];
                        ++tries;

                        //Does not seem to be able to pick new unique item
                        if (tries > 5)
                            return _set.ToList();
                    }

                } while (_set.Count <= _preCount);
            }

            return _set.ToList();
        }

        //Pick a number of items, all with unique indexes, from a list of TItem
        public List<TItem> UniqueIndexPickFromList<TItem>(int tryNrOfItems, List<TItem> list)
             where TItem : new()
        {
            //Create a hashed list of unique indexes
            HashSet<int> _set = new HashSet<int>();

            while (_set.Count < tryNrOfItems)
            {
                var _idx = this.Next(0, list.Count);

                int _preCount = _set.Count();
                int tries = 0;
                do
                {
                    _set.Add(_idx);

                    if (_set.Count == _preCount)
                    {
                        //Idx was already in the _set. Generate a new one
                        _idx = this.Next(0, list.Count);
                        ++tries;

                        //Does not seem to be able to generate new unique idx
                        if (tries > 5)
                            break;
                    }

                } while (_set.Count <= _preCount);
            }

            //I have now a set of unique idx
            //return a list of items from a list with indexes
            var retList = new List<TItem>();
            foreach (var item in _set)
            {
                retList.Add(list[item]);
            }
            return retList;
        }
        #endregion
 
        #region initialize master content
        csSeedJsonContent CreateMasterSeedFile()
        {
            return new csSeedJsonContent()
            {
                _quotes = new List<csSeedQuote>
                {
                    //About Love
                    new csSeedQuote
                    {
                        jsonQuote = "Would I rather be feared or loved? Easy. Both. I want people to be afraid of how much they love me.",
                        jsonAuthor = "Michael Scott, The Office"},
                    new csSeedQuote
                    {
                        jsonQuote = "All you need is love. But a little chocolate now and then doesn’t hurt.",
                        jsonAuthor = "Charles M. Schulz"
                    },
                    new csSeedQuote
                    {
                        jsonQuote = "Before you marry a person, you should first make them use a computer with slow Internet to see who they really are.",
                        jsonAuthor = "Will Ferrell"
                    },
                    new csSeedQuote
                    {
                        jsonQuote = "I love being married. It’s so great to find one special person you want to annoy for the rest of your life.",
                        jsonAuthor = "Rita Rudner"
                    },
                    new csSeedQuote
                    {
                        jsonQuote = "If love is the answer, can you please rephrase the question?",
                        jsonAuthor = "Lily Tomlin"
                    },
                    new csSeedQuote
                    {
                        jsonQuote = "Love can change a person the way a parent can change a baby—awkwardly, and often with a great deal of mess.",
                        jsonAuthor = "Lemony Snicket"
                    },
                    new csSeedQuote
                    {
                        jsonQuote = "Love is a fire. But whether it is going to warm your hearth or burn down your house, you can never tell.",
                        jsonAuthor = "Joan Crawford"
                    },
                    new csSeedQuote
                    {
                        jsonQuote = "A successful marriage requires falling in love many times, always with the same person.",
                        jsonAuthor = "Mignon McLaughlin"
                    },
                    new csSeedQuote
                    {
                        jsonQuote = "I love you with all my belly. I would say my heart, but my belly is bigger.",
                        jsonAuthor = "Unknown"
                    },
                    new csSeedQuote
                    {
                        jsonQuote = "The four most important words in any marriage—I’ll do the dishes.",
                        jsonAuthor = "Unknown"
                    },
                    new csSeedQuote
                    {
                        jsonQuote = "I love you more than coffee but not always before coffee.",
                        jsonAuthor = "Unknown"
                    },
                    new csSeedQuote
                    {
                        jsonQuote = "You know that tingly little feeling you get when you like someone? That’s your common sense leaving your body.",
                        jsonAuthor = "Unknown"
                    },

                    //About Work
                    new csSeedQuote
                    {
                        jsonQuote = "I choose a lazy person to do a hard job, because a lazy person will find an easy way to do it.",
                        jsonAuthor = "Bill Gates"
                    },
                    new csSeedQuote
                    {
                        jsonQuote = "Doing nothing is very hard to do… you never know when you’re finished.",
                        jsonAuthor = "Leslie Nielsen"
                    },
                    new csSeedQuote
                    {
                        jsonQuote = "It takes less time to do a thing right, than it does to explain why you did it wrong.",
                        jsonAuthor = "Henry Wadsworth Longfellow"
                    },
                    new csSeedQuote
                    {
                        jsonQuote = "Most of what we call management consists of making it difficult for people to get their work done.",
                        jsonAuthor = "Peter Drucker"
                    },
                    new csSeedQuote
                    {
                        jsonQuote = "It is better to have one person working with you than three people working for you.",
                        jsonAuthor = "Dwight D. Eisenhower"
                    },
                    new csSeedQuote
                    {
                        jsonQuote = "The best way to appreciate your job is to imagine yourself without one.",
                        jsonAuthor = "Oscar Wilde"
                    },
                    new csSeedQuote
                    {
                        jsonQuote = "I hate when I lose things at work, like pens, papers, sanity and dreams.",
                        jsonAuthor = "Unknown"
                    },
                    new csSeedQuote
                    {
                        jsonQuote = "Creativity is allowing yourself to make mistakes. Art is knowing which ones to keep.",
                        jsonAuthor = "Scott Adams"
                    },
                    new csSeedQuote
                    {
                        jsonQuote = "My keyboard must be broken, I keep hitting the escape key, but I’m still at work.",
                        jsonAuthor = "Unknown"
                    },
                    new csSeedQuote
                    {
                        jsonQuote = "Work is against human nature. The proof is that it makes us tired.",
                        jsonAuthor = "Michel Tournier"
                    },
                    new csSeedQuote
                    {
                        jsonQuote = "The reward for good work is more work.",
                        jsonAuthor = "Francesca Elisia"
                    },
                    new csSeedQuote
                    {
                        jsonQuote = "Executive ability is deciding quickly and getting somebody else to do the work.",
                        jsonAuthor = "Earl Nightingale"
                    },

                    //About Procrastination
                    new csSeedQuote
                    {
                        jsonQuote = "I never put off till tomorrow what I can do the day after.",
                        jsonAuthor = "Oscar Wilde"
                    },
                    new csSeedQuote
                    {
                        jsonQuote = "I think of myself as something of a connoisseur of procrastination, creative and dogged in my approach to not getting things done.",
                        jsonAuthor = "Susan Orlean"
                    },
                    new csSeedQuote
                    {
                        jsonQuote = "Procrastination is like a credit card: it's a lot of fun until you get the bill.",
                        jsonAuthor = "Christopher Parker"
                    },
                    new csSeedQuote
                    {
                        jsonQuote = "Nothing says work efficiency like panic mode.",
                        jsonAuthor = "Don Roff"
                    },
                    new csSeedQuote
                    {
                        jsonQuote = "I'm going to stop putting things off, starting tomorrow!",
                        jsonAuthor = "Sam Levenson"
                    },
                    new csSeedQuote
                    {
                        jsonQuote = "Procrastination always gives you something to look forward to.",
                        jsonAuthor = "Joan Konner"
                    },
                    new csSeedQuote
                    {
                        jsonQuote = "The time you enjoy wasting is not wasted time.",
                        jsonAuthor = "Bertrand Russell"
                    },
                    new csSeedQuote
                    {
                        jsonQuote = "Procrastination is the art of keeping up with yesterday.",
                        jsonAuthor = "Don Marquis"
                    },
                    new csSeedQuote
                    {
                        jsonQuote = "If it weren't for the last minute, nothing would get done.",
                        jsonAuthor = "Rita Mae Brown"
                    },
                    new csSeedQuote
                    {
                        jsonQuote = "I like work; it fascinates me. I can sit and look at it for hours.",
                        jsonAuthor = "Jerome K. Jerome"
                    },
                    new csSeedQuote
                    {
                        jsonQuote = "Procrastination isn't the problem. It's the solution. It's the universe's way of saying stop, slow down, you move too fast.",
                        jsonAuthor = "Ellen DeGeneres"
                    },
                    new csSeedQuote
                    {
                        jsonQuote = "Procrastinate now, don't put it off.",
                        jsonAuthor = "Ellen DeGeneres"
                    }
                },
                _latin = new List<csSeedLatin> {
                        new csSeedLatin {  jsonParagraph =
                            "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Duis convallis convallis tellus id interdum velit laoreet id donec. Sem nulla pharetra diam sit amet nisl.Id porta nibh venenatis cras sed felis eget. Non quam lacus suspendisse faucibus interdum posuere lorem. Vitae purus faucibus ornare suspendisse sed nisi lacus sed.Sapien faucibus et molestie ac feugiat sed lectus vestibulum.Ornare lectus sit amet est placerat in egestas.Eu consequat ac felis donec et. Placerat orci nulla pellentesque dignissim enim sit.Cras ornare arcu dui vivamus arcu. Diam quis enim lobortis scelerisque fermentum. Rutrum quisque non tellus orci ac auctor augue. Fringilla est ullamcorper eget nulla facilisi. Dui accumsan sit amet nulla.Diam maecenas sed enim ut sem viverra aliquet." },
                        new csSeedLatin { jsonParagraph =
                            "Vitae tempus quam pellentesque nec nam. Dictumst quisque sagittis purus sit amet volutpat consequat mauris nunc. Vitae congue eu consequat ac. Condimentum mattis pellentesque id nibh tortor id aliquet lectus proin. Tempus egestas sed sed risus pretium quam vulputate dignissim. Quis commodo odio aenean sed adipiscing diam. Egestas congue quisque egestas diam in arcu cursus. Sem et tortor consequat id porta nibh venenatis cras sed. Ultrices neque ornare aenean euismod elementum nisi. Lectus magna fringilla urna porttitor rhoncus dolor purus non enim. Nibh sit amet commodo nulla. Odio facilisis mauris sit amet massa vitae tortor condimentum lacinia. Adipiscing commodo elit at imperdiet dui accumsan sit. Ultrices sagittis orci a scelerisque purus semper eget duis at." },
                        new csSeedLatin { jsonParagraph =
                            "Tincidunt tortor aliquam nulla facilisi cras. Commodo odio aenean sed adipiscing diam. Justo donec enim diam vulputate ut pharetra sit. Nulla at volutpat diam ut venenatis tellus in. Enim nec dui nunc mattis enim ut tellus elementum. Quis viverra nibh cras pulvinar. Cras fermentum odio eu feugiat. Pretium vulputate sapien nec sagittis aliquam malesuada bibendum arcu. Risus nec feugiat in fermentum posuere urna nec tincidunt praesent. A condimentum vitae sapien pellentesque habitant morbi tristique. Sed vulputate mi sit amet mauris commodo quis imperdiet. Mauris pharetra et ultrices neque."},
                        new csSeedLatin { jsonParagraph =
                            "Etiam dignissim diam quis enim lobortis scelerisque fermentum. Vel quam elementum pulvinar etiam non quam lacus suspendisse. Purus ut faucibus pulvinar elementum integer enim neque volutpat. Vulputate ut pharetra sit amet aliquam. Amet nisl suscipit adipiscing bibendum est. Velit laoreet id donec ultrices tincidunt arcu. Purus in massa tempor nec feugiat. Vulputate enim nulla aliquet porttitor. Amet consectetur adipiscing elit pellentesque habitant morbi tristique senectus. Sociis natoque penatibus et magnis dis parturient montes nascetur ridiculus. Fermentum odio eu feugiat pretium. Molestie ac feugiat sed lectus. Mattis rhoncus urna neque viverra. Mollis nunc sed id semper risus in. Amet venenatis urna cursus eget. Consequat ac felis donec et odio pellentesque diam." },
                        new csSeedLatin { jsonParagraph =
                            "Facilisi nullam vehicula ipsum a arcu cursus. Metus vulputate eu scelerisque felis imperdiet proin. Ultrices dui sapien eget mi proin sed libero enim. Enim eu turpis egestas pretium aenean. Nibh praesent tristique magna sit amet. Urna cursus eget nunc scelerisque viverra mauris in. A erat nam at lectus urna duis convallis convallis. Nullam eget felis eget nunc. Bibendum est ultricies integer quis auctor elit sed. Quis enim lobortis scelerisque fermentum dui faucibus. Fermentum et sollicitudin ac orci phasellus egestas tellus. Odio morbi quis commodo odio aenean sed adipiscing. Viverra suspendisse potenti nullam ac tortor vitae purus. Proin fermentum leo vel orci porta non pulvinar neque laoreet. Aliquam faucibus purus in massa tempor nec feugiat nisl. Aliquet eget sit amet tellus cras adipiscing enim eu. Nascetur ridiculus mus mauris vitae. Adipiscing elit ut aliquam purus sit." },
                        new csSeedLatin { jsonParagraph =
                            "Justo donec enim diam vulputate ut pharetra sit amet. Quis eleifend quam adipiscing vitae proin sagittis nisl rhoncus. Nunc eget lorem dolor sed viverra ipsum nunc. Enim ut tellus elementum sagittis vitae et. Quam pellentesque nec nam aliquam sem et. Vestibulum lorem sed risus ultricies. Proin libero nunc consequat interdum varius. Hac habitasse platea dictumst vestibulum rhoncus est pellentesque. Lobortis mattis aliquam faucibus purus in massa tempor nec feugiat. Pellentesque nec nam aliquam sem et tortor consequat. Augue mauris augue neque gravida in fermentum et sollicitudin ac. Egestas egestas fringilla phasellus faucibus scelerisque eleifend donec. Morbi tristique senectus et netus et malesuada. Lacinia at quis risus sed vulputate odio ut enim blandit. Lacus sed viverra tellus in hac habitasse platea dictumst vestibulum. Ipsum suspendisse ultrices gravida dictum fusce ut placerat" },
                        new csSeedLatin { jsonParagraph =
                            "Sed tempus urna et pharetra pharetra massa massa ultricies. Gravida in fermentum et sollicitudin ac. Praesent tristique magna sit amet purus gravida quis. Scelerisque purus semper eget duis at tellus at. Odio euismod lacinia at quis risus. Platea dictumst quisque sagittis purus sit amet. Ultrices sagittis orci a scelerisque purus. Arcu dui vivamus arcu felis bibendum ut. Fames ac turpis egestas maecenas pharetra convallis. Consectetur adipiscing elit pellentesque habitant morbi tristique senectus. Eget gravida cum sociis natoque. Enim blandit volutpat maecenas volutpat blandit. Laoreet sit amet cursus sit amet." },
                        new csSeedLatin { jsonParagraph =
                            "Vestibulum lectus mauris ultrices eros in cursus. Nec ultrices dui sapien eget mi proin sed libero enim. Senectus et netus et malesuada fames. Facilisis sed odio morbi quis. In tellus integer feugiat scelerisque. Cras adipiscing enim eu turpis egestas. Ut eu sem integer vitae justo. Donec ac odio tempor orci. Etiam sit amet nisl purus in. Habitant morbi tristique senectus et netus et malesuada fames. Sed elementum tempus egestas sed sed risus pretium. Non nisi est sit amet facilisis. Tempus imperdiet nulla malesuada pellentesque elit. Libero enim sed faucibus turpis in eu mi bibendum. In fermentum et sollicitudin ac orci phasellus egestas tellus. Dictumst vestibulum rhoncus est pellentesque elit ullamcorper. Nisl pretium fusce id velit ut tortor pretium." },
                        new csSeedLatin { jsonParagraph =
                            "Est pellentesque elit ullamcorper dignissim cras tincidunt lobortis. Dignissim diam quis enim lobortis scelerisque. Sit amet commodo nulla facilisi nullam vehicula. Ut etiam sit amet nisl purus. Fusce ut placerat orci nulla pellentesque. Cras pulvinar mattis nunc sed blandit. A erat nam at lectus urna duis convallis. Dictum at tempor commodo ullamcorper a lacus vestibulum sed. Tempus egestas sed sed risus pretium quam vulputate dignissim suspendisse. Viverra accumsan in nisl nisi scelerisque eu ultrices. Blandit aliquam etiam erat velit scelerisque in dictum non. Quam elementum pulvinar etiam non. Odio tempor orci dapibus ultrices in iaculis nunc sed augue. Venenatis urna cursus eget nunc scelerisque viverra. Sit amet mattis vulputate enim nulla aliquet porttitor lacus. Scelerisque felis imperdiet proin fermentum. Tellus in metus vulputate eu. Amet venenatis urna cursus eget nunc scelerisque viverra mauris. Pharetra vel turpis nunc eget lorem. Mauris rhoncus aenean vel elit scelerisque mauris." },
                        new csSeedLatin { jsonParagraph =
                            "A scelerisque purus semper eget duis at. Tristique risus nec feugiat in fermentum. Eu tincidunt tortor aliquam nulla facilisi cras fermentum odio. Enim nec dui nunc mattis enim. Tincidunt lobortis feugiat vivamus at augue. Magna eget est lorem ipsum dolor. Auctor elit sed vulputate mi. Egestas egestas fringilla phasellus faucibus scelerisque eleifend donec pretium vulputate. Pretium viverra suspendisse potenti nullam ac tortor vitae. Amet risus nullam eget felis. Dolor sed viverra ipsum nunc aliquet bibendum enim facilisis gravida. Mi eget mauris pharetra et. Pharetra convallis posuere morbi leo. Justo eget magna fermentum iaculis eu non diam phasellus vestibulum. In mollis nunc sed id semper risus. Bibendum at varius vel pharetra vel turpis." },
            },
                _addresses = new List<csSeedAddress>
                {
                        new csSeedAddress {
                            jsonCountry = "Sweden",
                            jsonCities = "Stockholm, Göteborg, Malmö, Uppsala, Linköping, Örebro",
                            jsonStreets = "Svedjevägen, Ringvägen, Vasagatan, Odenplan, Birger Jarlsgatan, Äppelviksvägen, Kvarnbacksvägen"
                        },
                        new csSeedAddress {
                            jsonCountry = "Norway",
                            jsonCities = "Oslo, Bergen, Trondheim, Stavanger, Dramen",
                            jsonStreets = "Bygdoy alle, Frognerveien, Pilestredet, Vidars gate, Sågveien, Toftes gate, Gardeveiend",
                    },
                        new csSeedAddress {
                            jsonCountry = "Denmark",
                            jsonCities = "Köpenhamn, Århus, Odense, Aahlborg, Esbjerg",
                            jsonStreets = "Rolighedsvej, Fensmarkgade, Svanevej, Gröndalsvej, Githersgade, Classensgade, Moltekesvej"
                    },
                        new csSeedAddress {
                            jsonCountry = "Finland",
                            jsonCities = "Helsingfors, Espoo, Tampere, Vaanta, Oulu",
                            jsonStreets = "Arkandiankatu, Liisankatu, Ruoholahdenkatu, Pohjoistranta, Eerikinkatu, Vauhtitie, Itainen Vaideki"
                    },
                },
                _names = new csSeedNames
                {
                    jsonFirstNames = "Harry, Lord, Hermione, Albus, Severus, Ron, Draco, Frodo, Gandalf, Sam, Peregrin, Saruman",
                    jsonLastNames = "Potter, Voldemort, Granger, Dumbledore, Snape, Malfoy, Baggins, the Gray, Gamgee, Took, the White",
                    jsonPetNames = "Max, Charlie, Cooper, Milo, Rocky, Wanda, Teddy, Duke, Leo, Max, Simba",
                },
                _domains = new csSeedDomains
                {
                    jsonDomainNames = "icloud.com, me.com, mac.com, hotmail.com, gmail.com"
                },
                _music = new csSeedMusic
                {
                    jsonGroupNames = "Led, Zeppelin, Queen, Pink, Floyd, Creedence, Clearwater, Revival, " +
                        "Arosmith, Who, AC/DC, Rolling, Stones, Eagles, Deep, Purple, Prince, Dylan",
                    jsonAlbumNames = "Heaven, Rock, Moon, Cosmos, Walk, Hunky, Blue, Highway, " +
                        "Satisfaction, California, Stairway, Purple, Senor",
                    jsonAlbumPrefix = "A, The, One, The great, A wonderful, Let's rock with, Relaxing, Chill with,  ",
                    jsonAlbumSuffix = "with friends, with love, with fire, and walking, being happy",
                }
            };
        }
        #endregion

        #region create master json file
        public void WriteMasterStream()
        {
            CreateMasterSeedFile().WriteFile("master-seeds.json");
        }
        #endregion

        #region contructors
        public csSeedGenerator()
        {
            _seeds = CreateMasterSeedFile();
        }
        public csSeedGenerator(string SeedFileName)
        {
            if (!csSeedJsonContent.FileExists(SeedFileName))
            {
                throw new FileNotFoundException(SeedFileName);
            }
            _seeds = csSeedJsonContent.ReadFile(SeedFileName);
        }
        #endregion

        #region internal classes
        class csSeedLatin
        {
            #region Latin towards json file
            string _jsonParagraph;
            public string jsonParagraph
            {
                get => _jsonParagraph;
                set
                {
                    _jsonParagraph = value;
                    _sentences = new List<string>(_jsonParagraph.Split(". "))
                        .Select(s =>
                        {
                            var _sentence = s.Trim(new char[] { ' ', ',', '.' });
                            return _sentence + '.';
                        }).ToList();

                    _words = new List<string>(_jsonParagraph.Split(" "))
                        .Select(w => w.Trim(new char[] { ' ', ',', '.' })).ToList();
                }
            }
            #endregion

            [JsonIgnore]
            public string Paragraph => _jsonParagraph;

            List<string> _sentences;
            [JsonIgnore]
            public List<string> Sentences => _sentences;

            List<string> _words;
            [JsonIgnore]
            public List<string> Words => _words;
        }
        class csSeedQuote
        {
            #region Quotes towards json file
            string _jsonQuote;
            public string jsonQuote { get => _jsonQuote; set => _jsonQuote = value; }

            string _jsonAuthor;
            public string jsonAuthor { get => _jsonAuthor; set => _jsonAuthor = value; }
            #endregion

            [JsonIgnore]
            public string Quote => _jsonQuote;
            [JsonIgnore]
            public string Author => _jsonAuthor;
        }
        class csSeedAddress
        {
            #region Country towards json file
            string _jsonCountry;
            public string jsonCountry
            {
                get => _jsonCountry;
                set
                {
                    _jsonCountry = value;
                }
            }
            #endregion

            public string Country => _jsonCountry;

            #region Streets towards json file
            string _jsonStreets;
            public string jsonStreets
            {
                get => _jsonStreets;
                set
                {
                    _jsonStreets = value;
                    _streets = _jsonStreets.Split(", ").ToList();
                }
            }
            #endregion

            List<string> _streets;
            [JsonIgnore]
            public List<string> Streets => _streets;

            #region Cities towards json file
            string _jsonCities;
            public string jsonCities
            {
                get => _jsonCities;
                set
                {
                    _jsonCities = value;
                    _cities = _jsonCities.Split(", ").ToList();
                }
            }
            #endregion

            List<string> _cities;
            [JsonIgnore]
            public List<string> Cities => _cities;
        }
        class csSeedNames
        {
            #region Names towards json file
            string _jsonFirstNames;
            public string jsonFirstNames
            {
                get => _jsonFirstNames;
                set
                {
                    _jsonFirstNames = value;
                    _firstNames = _jsonFirstNames.Split(", ").ToList();
                }
            }

            string _jsonLastNames;
            public string jsonLastNames
            {
                get => _jsonLastNames;
                set
                {
                    _jsonLastNames = value;
                    _lastNames = _jsonLastNames.Split(", ").ToList();
                }
            }

            string _jsonPetNames;
            public string jsonPetNames
            {
                get => _jsonPetNames;
                set
                {
                    _jsonPetNames = value;
                    _petNames = _jsonPetNames.Split(", ").ToList();
                }
            }
            #endregion

            List<string> _firstNames;
            [JsonIgnore]
            public List<string> FirstNames => _firstNames;

            List<string> _lastNames;
            [JsonIgnore]
            public List<string> LastNames => _lastNames;

            List<string> _petNames;
            [JsonIgnore]
            public List<string> PetNames => _petNames;
        }
        class csSeedDomains
        {
            #region Domains towards json file
            string _jsonDomainNames;
            public string jsonDomainNames
            {
                get => _jsonDomainNames;
                set
                {
                    _jsonDomainNames = value;
                    _domainNames = _jsonDomainNames.Split(", ").ToList();
                }
            }
            #endregion

            List<string> _domainNames;
            [JsonIgnore]
            public List<string> Domains => _domainNames;
        }
        class csSeedMusic
        {
            #region Music towards json file
            string _jsonGroupNames;
            public string jsonGroupNames
            {
                get => _jsonGroupNames;
                set
                {
                    _jsonGroupNames = value;
                    _groupNames = _jsonGroupNames.Split(", ").ToList();
                }
            }

            string _jsonAlbumNames;
            public string jsonAlbumNames
            {
                get => _jsonAlbumNames;
                set
                {
                    _jsonAlbumNames = value;
                    _albumNames = _jsonAlbumNames.Split(", ").ToList();
                }
            }

            string _jsonAlbumPrefix;
            public string jsonAlbumPrefix
            {
                get => _jsonAlbumPrefix;
                set
                {
                    _jsonAlbumPrefix = value;
                    _albumPrefix = _jsonAlbumPrefix.Split(", ").ToList();
                }
            }

            string _jsonAlbumSuffix;
            public string jsonAlbumSuffix
            {
                get => _jsonAlbumSuffix;
                set
                {
                    _jsonAlbumSuffix = value;
                    _albumSuffix = _jsonAlbumSuffix.Split(", ").ToList();
                }
            }
            #endregion

            List<string> _groupNames;
            [JsonIgnore]
            public List<string> GroupNames => _groupNames;

            List<string> _albumNames;
            [JsonIgnore]
            public List<string> AlbumNames => _albumNames;

            List<string> _albumPrefix;
            [JsonIgnore]
            public List<string> AlbumPrefix => _albumPrefix;

            List<string> _albumSuffix;
            [JsonIgnore]
            public List<string> AlbumSuffix => _albumSuffix;
        }

        class csSeedJsonContent
        {
            public List<csSeedQuote> _quotes { get; set; } = new List<csSeedQuote>();
            public List<csSeedLatin> _latin { get; set; } = new List<csSeedLatin>();
            public List<csSeedAddress> _addresses { get; set; } = new List<csSeedAddress>();
            public csSeedNames _names { get; set; } = new csSeedNames();
            public csSeedDomains _domains { get; set; } = new csSeedDomains();
            public csSeedMusic _music { get; set; } = new csSeedMusic();


            public void WriteFile(string FileName) => WriteFile(this, FileName);
            public static void WriteFile(csSeedJsonContent Seeds, string FileName)
            {
                using (Stream s = File.Create(fname(FileName)))
                using (TextWriter writer = new StreamWriter(s))
                    writer.Write(JsonSerializer.Serialize<csSeedJsonContent>(Seeds, new JsonSerializerOptions() { WriteIndented = true }));
            }

            public static csSeedJsonContent ReadFile(string FileName)
            {
                csSeedJsonContent _seeds = null;
                using (Stream s = File.OpenRead(fname(FileName)))
                using (TextReader reader = new StreamReader(s))

                    _seeds = JsonSerializer.Deserialize<csSeedJsonContent>(reader.ReadToEnd());

                return _seeds;
            }

            static string fname(string name)
            {
                var documentPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
                documentPath = Path.Combine(documentPath, "SeedGenerator");
                if (!Directory.Exists(documentPath)) Directory.CreateDirectory(documentPath);
                return Path.Combine(documentPath, name);
            }

            public static bool FileExists(string FileName) => File.Exists(fname(FileName));
        }
        #endregion
    }
}

