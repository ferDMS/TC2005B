DROP SCHEMA if exists booksDB;

CREATE SCHEMA booksDB;

USE booksDB;

CREATE TABLE books (
    id_book INT AUTO_INCREMENT NOT NULL,
    title VARCHAR(50) NOT NULL,
    author VARCHAR(100) NOT NULL,
    cover VARCHAR(100) NOT NULL,
    PRIMARY KEY (id_book)
);

CREATE TABLE pages (
    id_page INT AUTO_INCREMENT NOT NULL,
    id_book INT NOT NULL,
    page_num INT NOT NULL,
    content VARCHAR(4000) NOT NULL,
    PRIMARY KEY (id_page),
    FOREIGN KEY (id_book) REFERENCES books(id_book)
);

-- Books procedures
-- ----------------------------------------

DELIMITER //

CREATE PROCEDURE `booksDB`.`getAllBooks`()
BEGIN
    SELECT * FROM books;
END//

CREATE PROCEDURE `booksDB`.`getBook`(IN _id_book INT)
BEGIN
    SELECT * FROM books WHERE id_book = _id_book;
END//

CREATE PROCEDURE `booksDB`.`updateBook`(IN _id_book INT, IN new_title VARCHAR(50), IN new_author VARCHAR(100), IN new_cover VARCHAR(100))
BEGIN
    UPDATE books SET title = new_title, author = new_author, cover = new_cover WHERE id_book = _id_book;
END//

CREATE PROCEDURE `booksDB`.`insertBook`(IN _title VARCHAR(50), IN _author VARCHAR(100), IN _cover VARCHAR(100))
BEGIN
	INSERT INTO books(title, author, cover) VALUES (_title, _author, _cover);
END//

CREATE PROCEDURE `booksDB`.`deleteBook`(IN _id_book INT)
BEGIN
	DELETE FROM books WHERE books.id = _id_book;
END//

-- --------------------------------------------------------
-- Pages procedures

CREATE PROCEDURE `booksDB`.`getBookAllPages`(IN _id_book INT)
BEGIN
    SELECT * FROM pages WHERE id_book = _id_book;
END//

CREATE PROCEDURE `booksDB`.`getBookPage`(IN _id_book INT, IN _page_num INT)
BEGIN
	SELECT * FROM pages WHERE id_book = _id_book AND page_num = _page_num;
END//

CREATE PROCEDURE `booksDB`.`updateBookPage`(IN _id_book INT, IN _page_num INT, IN new_content VARCHAR(4000))
BEGIN
    UPDATE pages SET content = new_content WHERE page_num = _page_num AND id_book = _id_book;
END//

CREATE PROCEDURE `booksDB`.`insertBookPage`(IN _id_book INT, IN _content VARCHAR(4000))
BEGIN
	-- Find the next page number
	DECLARE next_page_num INT;
    SELECT COALESCE(MAX(page_num), 0) + 1 INTO next_page_num 
    FROM pages 
    WHERE id_book = _id_book;
   
    INSERT INTO pages (id_book, page_num, content) 
    VALUES (_id_book, next_page_num, _content);
END//

CREATE PROCEDURE `booksDB`.`deleteBookPage`(IN _id_book INT, IN _page_num INT)
BEGIN
    DELETE FROM pages WHERE page_num = _page_num AND id_book = _id_book;
END//

DELIMITER ;

USE booksDB;

INSERT INTO books (title, author, cover) VALUES 
("Harry Potter and the Sorcerer's Stone", "J. K. Rowling", 'https://media.harrypotterfanzone.com/sorcerer-stone-childrens-tenth-anniversary-edition.jpg'),
("Harry Potter and the Chamber of Secrets", "J. K. Rowling", 'https://media.harrypotterfanzone.com/chamber-of-secrets-uk-childrens-edition-2014.jpg'),
("Harry Potter and the Prisoner of Azkaban", "J. K. Rowling", 'https://media.harrypotterfanzone.com/prisoner-of-azkaban-uk-childrens-edition-2014.jpg'),
("Harry Potter and the Goblet of Fire", "J. K. Rowling", 'https://media.harrypotterfanzone.com/goblet-of-fire-adult-edition.jpg'),
("Harry Potter and the Order of the Phoenix", "J. K. Rowling", 'https://media.harrypotterfanzone.com/order-of-the-phoenix-us-childrens-edition-2013.jpg'),
("Harry Potter and the Half-Blood Prince", "J. K. Rowling", 'https://media.harrypotterfanzone.com/half-blood-prince-adult-edition.jpg'),
("Harry Potter and the Deathly Hallows", "J. K. Rowling", 'https://media.harrypotterfanzone.com/deathly-hallows-signature-edition.jpg');

CALL booksDB.insertBookPage(1, 
  'Mr. and Mrs. Dursley, of number four, Privet Drive, were proud to say that they were perfectly normal, thank you very much. They even got a bit annoyed when the milkman delivered cream instead of skimmed one morning, when Mrs. Dursley was particularly busy trying to make perfect meringues. The Dursleys had everything they wanted, but they also had a secret, and their greatest fear was that somebody would discover it. They didn\'t think they could bear it if anyone found out about the Potters. Mrs. Potter was Mrs. Dursley\'s sister, but they hadn\'t met for several years; in fact, Mrs. Dursley pretended she didn\'t have a sister, because her sister was not normal.');
CALL booksDB.insertBookPage(1, 
  'The Dursleys shuddered to think what the neighbors would say. The Dursleys knew the Potters had been dead for quite some time. Mrs. Dursley shuddered to think what her sister had probably been up to before she died. Potters weren\'t folks you enforced the law on, in any case. The Dursleys shuddered to think what the neighbors would say. Still, they couldn\'t help wondering what had happened to the Potters\' son, Harry. On their nephew, they\'d never spoken a word. It was setting a bad example, they sternly reminded themselves.');
CALL booksDB.insertBookPage(1, 
  'The Dursleys had tried, of course, just as hard as they could, to pretend Harry didn\'t exist. When Harry had been a baby and hadn\'t stopped crying for days, Mr. Dursley had threatened to throw him out the window, but Mrs. Dursley had begged him not to; she\'d had enough trouble with the neighbors without that. So they had put Harry in a cupboard under the stairs and locked the door. He hadn\'t cried for a long time after that. He knew there was no point. Aunt Petunia wouldn\'t let him out, even to go to the toilet.');
CALL booksDB.insertBookPage(1, 
  'Harry couldn\'t remember a time when he hadn\'t lived in the cupboard. He knew that Dudley had gotten the room with the window, and he knew there had been a time when he\'d been in a nursery, but all that blurred in memory. The cupboard under the stairs was his world, and he knew every bump on the rough wooden walls. He knew where the floorboards creaked, and exactly how loose the latch was on the door.');
CALL booksDB.insertBookPage(1, 
  'The Dursleys couldn\'t understand what Harry found there to be so attractive. It was dark and dusty; spiders spun webs in the corners. He wouldn\'t be able to fit a toy box in there, and the only light came in through a crack in the door. But it was his, and it was all he had. Harry got in and pulled the loose floorboard back into place. He sat on the floor and pulled his knees up to his chest; with nothing to look at but the peeling wallpaper, he began to think again about the very strange happenings of the last few weeks.');

CALL booksDB.insertBookPage(2, 
  'The summer holidays were drawing to a close and the Dursleys were planning a grand finale – a trip to the seaside for Dudley\'s birthday. Harry Potter, unwanted son of Mr. and Mrs. Dursley, would naturally not be invited. Mrs. Dursley pretended she couldn\'t afford to take him; Mr. Dursley grumbled about the cost of extra food. The truth, of course, was that they didn\'t want Harry tagging along.');
CALL booksDB.insertBookPage(2, 
  'The Dursleys knew Harry hated the seaside. He never enjoyed the cold wind whipping his hair in his face, nor the salty spray that made his glasses blurry, nor the feeling of sand between his toes. But none of that mattered to the Dursleys. They were going to enjoy themselves, and they didn\'t care how miserable Harry was.');
CALL booksDB.insertBookPage(2, 
  'On their last morning at the seaside, Harry was left in Dudley\'s and Piers\' care while Mr. and Mrs. Dursley went shopping. A fight quickly broke out over a bucket and spade, and Harry, as always, got the blame. Uncle Vernon, furious, decided to punish Harry by making him stay in his room for the whole of the next day.');
CALL booksDB.insertBookPage(2, 
  'Harry lay on his bed with nothing to look forward to but a long, dull afternoon. He stared out of the tiny window, feeling very much like a caged bird.  That evening, just as the Dursleys sat down to a delicious-looking dinner of roast beef, there was a tapping on the window. Harry peered through the curtains. A large, snowy owl was fluttering outside.');
CALL booksDB.insertBookPage(2, 
  'Harry watched, frozen, as the owl swooped down, landing next to the open window. It had a long, cream-colored envelope tied to its leg. Before Harry could move, the owl had hopped back inside, dropped the envelope on Harry\'s pillow, and hooted twice loudly before taking off again. Harry stared at the envelope. There was no stamp, and no writing on the front except an address in emerald green ink: Mr. H. Potter, The Room Under the Stairs, Number Four, Privet Drive.');

CALL booksDB.insertBookPage(3, 
  'Aunt Marge, a great aunt on his mother\'s side, was coming to stay. Harry, who loathed Aunt Marge more than he loathed Dudley, didn\'t need a crystal ball to see that this was going to be a horrible summer.  Aunt Marge was a huge woman with a booming laugh and a fondness for drinks that made her rheumy eyes water even more than usual.');
CALL booksDB.insertBookPage(3, 
  'The Dursleys, of course, were thrilled. They couldn\'t wait to show Marge how normal Harry was. Normal for them meant no bursts of accidental magic, no talking to snakes, and no friends with flying motorbikes.  Harry, on the other hand, knew he wouldn\'t last a week without doing something that would make Aunt Marge\'s eyes pop out of her head.');
CALL booksDB.insertBookPage(3, 
  'The very first morning, while Harry was helping Uncle Vernon weed the flowerbed, disaster struck. As Harry, lost in thought, was pulling up a particularly stubborn weed, he accidentally uprooted the rose bush next to it.  Uncle Vernon shrieked as if he\'d been stabbed in the stomach. Aunt Marge, who was watching from the kitchen window, roared with laughter.');
CALL booksDB.insertBookPage(3, 
  'Harry, on the other hand, felt anything but amused. Uncle Vernon’s face was turning a puce color, and a vein in his temple was throbbing like a worm trying to escape.  "See!" he bellowed at Harry. "See what you\'ve done! You\'ve ruined Marge\'s favorite rose bush!"');
CALL booksDB.insertBookPage(3, 
  '"It\'s not my fault," Harry said sullenly.  Aunt Marge, still wheezing with laughter, lumbered out of the kitchen. "Don\'t you worry about it, Vernon," she boomed. "I\'ll mend it. Remind me of a good dose of dung fertilizer – always works wonders with these stubborn magical plants."');

CALL booksDB.insertBookPage(4, 
  'The Dursleys were past caring whether Harry liked their guest or not. They were just pleased to have a prestigious visitor who thought they were perfectly normal, thank you very much. Mrs. Polyspreafe, a plump and rosy-cheeked woman with a booming laugh, thought they were delightful. The Dursleys, of course, were hospitable to a fault.');
CALL booksDB.insertBookPage(4, 
  'Harry, however, would much rather Mrs. Polyspreafe had stayed away. It was bad enough having the Dursleys watching his every move; now he had this nosy woman to contend with as well. Mrs. Polyspreafe seemed to find Harry endlessly fascinating – or rather, she found the fact that he was a wizard endlessly fascinating.');
CALL booksDB.insertBookPage(4, 
  'She insisted on asking him questions about Hogwarts School of Witchcraft and Wizardry and would cackle with delight at his replies. The Dursleys, on the other hand, couldn\'t hide their disgust. Uncle Vernon grunted a lot and glared at Harry over his spectacles; Aunt Petunia looked as though she might faint at any moment.');
CALL booksDB.insertBookPage(4, 
  'Harry couldn\'t wait for Mrs. Polyspreafe to leave, but unfortunately, it seemed she intended to stay for a while. Every evening, she would settle down on the sofa next to Harry, bombard him with questions, and then, when it was time for bed, she would waddle up the stairs behind him.');
CALL booksDB.insertBookPage(4, 
  'One evening, Mrs. Polyspreafe was particularly tiresome. Harry, feeling more and more trapped by the second, longed to escape to his room.  "So, you say this friend of yours can turn into a dog?" Mrs. Polyspreafe was asking, her eyes shining.');

CALL booksDB.insertBookPage(5, 
  'The summer holidays had arrived and Harry Potter was still locked up in his room at number four, Privet Drive.  He hadn\'t been allowed out for a month now. Ever since Mrs. Figg, the squib who lived next door, had seen him disappear on the night of Sirius Black\'s escape from Azkaban, Harry had been a prisoner in his own home.');
CALL booksDB.insertBookPage(5, 
  'Uncle Vernon had boarded up all the windows and barred the front door. Harry couldn\'t even answer the phone in case it was a wizard, and all his magical belongings had been confiscated.  He was not allowed to leave his room without an escort, and on those rare occasions he was permitted downstairs, he had to sit with Uncle Vernon watching Muggle news programs.');
CALL booksDB.insertBookPage(5, 
  'Harry couldn\'t see why the Dursleys were so worried. Sirius Black – whatever he\'d done – couldn\'t possibly be interested in them. There was nothing in their house that would tempt a dark wizard, no hidden gold, no magical artifacts, nothing except – a sudden, cold feeling filled Harry\'s insides. He remembered what Sirius had said about his parents.');
CALL booksDB.insertBookPage(5, 
  'Harry bolted to the cupboard under the stairs, the place where he\'d hidden all his Hogwarts things. He fumbled with the loose floorboard, heart pounding, and pulled out his trunk. Inside, nestled amongst his robes and textbooks, was a battered photo album. He flipped it open, his breath catching in his throat.');
CALL booksDB.insertBookPage(5, 
  'The album was full of photographs of his parents. Smiling back at him from a slightly cracked picture were a young James Potter and Lily Evans, both with messy dark hair and bright green eyes. Harry ran a finger over their smiling faces, a wave of longing washing over him. He missed them so much.');

  
CALL booksDB.insertBookPage(6, 
  'The summer holidays had begun and Harry Potter was still waiting for news from Hogwarts.  He sat at the kitchen table of number four, Privet Drive, with a bowl of lukewarm cereal in front of him.  He wasn\'t hungry.  A lump the size of his fist was lodged firmly in his throat.');
CALL booksDB.insertBookPage(6, 
  'He hadn\'t heard from any of his friends all summer.  His owl, Hedwig, was forbidden to leave the house and his other means of communication, Floo powder and the Weasley family\'s Extendable Ears, had been confiscated by Uncle Vernon.');
CALL booksDB.insertBookPage(6, 
  'The Dursleys, of course, were delighted.  They were convinced that Harry wouldn\'t be going back to Hogwarts this year.  Every morning, the Daily Mail would arrive with a fresh headline about \'Harry Potter\'s Downfall\' or \'The Boy Who Lied Expelled?\' Harry couldn\'t bear to read them.');
CALL booksDB.insertBookPage(6, 
  'The only bright spot in Harry\'s summer was the arrival of a new textbook, \'Advanced Potion-Making\'.  He opened it eagerly, hoping for a distraction, but a single scrawled sentence on the flyleaf made his heart sink.');
CALL booksDB.insertBookPage(6, 
  '\'This book is the property of the Half-Blood Prince\' was written in a very spidery hand.  Harry frowned, trying to remember if there had ever been a student at Hogwarts called \'The Half-Blood Prince\'. He couldn\'t remember.');

CALL booksDB.insertBookPage(7, 
  'The Burrow was a mess, and Harry liked it.  Furniture was piled everywhere, cushions were bursting from seams, and mismatched crockery littered the table.  The Weasley family, except for Percy, were crammed into the small sitting room, their faces grim.');
CALL booksDB.insertBookPage(7, 
  'Mr. Weasley, his face pale and tired, was stuffing his pockets with socks.  Mrs. Weasley was stuffing Patronus charms into every available space in her handbag.  Fred and George, their expressions unusually serious, were taking turns practicing Stunning Spells on a dented old saucepan.');
CALL booksDB.insertBookPage(7, 
  'Ron, looking lanky and awkward in a new pair of dress robes, was pacing the floor in front of the cold fireplace.  Ginny, her hair a fiery mess, was perched on the arm of a rickety armchair, biting her lip.');
CALL booksDB.insertBookPage(7, 
  'Harry sat on the floor, his school trunk packed beside him.  He felt a horrible mixture of excitement and dread.  Tonight, they were leaving.  Tonight, they were going into hiding.');
CALL booksDB.insertBookPage(7, 
  'The Ministry of Magic had fallen.  Scrimgeour was dead, murdered by Voldemort, and the Death Eaters were in control.  The Burrow, no longer a sanctuary, had become a nerve center of the Order of the Phoenix.');
