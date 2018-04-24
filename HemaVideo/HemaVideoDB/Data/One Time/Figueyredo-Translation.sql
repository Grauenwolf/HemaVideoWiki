INSERT INTO Translations.Translator
(
    TranslatorName,
    CreatedByUserKey,
    ModifiedByUserKey
)
VALUES
(N'Eric Myers and Steve Hick', -1, -1);

INSERT INTO Translations.Translation
(
    BookKey,
    TranslatorKey,
    Copyright,
    CreatedByUserKey,
    ModifiedByUserKey
)
VALUES
(3, 1,
 N'Users may, without further permission, display, save, and print this work for personal, non commercial use, provided that the copyright notice is not severed from the work. Libraries may store this material and non commercially redistribute it to their patrons in electronic or printed form for personal, non commercial use, provided that the copyright notice is not severed from the work.',
 -1 , -1);

DECLARE @TranslationKey INT;

SET @TranslationKey = SCOPE_IDENTITY();

DECLARE @Sections TABLE
(
    SectionKey INT,
    SectionName NVARCHAR(MAX),
    Translation NVARCHAR(MAX)
);

INSERT INTO @Sections
(
    SectionKey,
    SectionName,
    Translation
)
VALUES
('3002', 'Simple Rule 1', 'This first rule is the one which most reveals the elegance of the montante, and whoever performs it well will be able to perform them all. You will place your body straight with the left foot in front, the montante with the point on the ground, taken by the cross in the right hand with the thumb down, and you will tap it forward with the right foot, turning the montante to set it in place. Then you will give a talho from behind, from low to high, moving the right foot forward at the same time, and stopping with the montante in right angle in front of the face. From there you will remove the montante to give a revez cutting from behind with the other edge of the montante, also from low to high, and at the same time moving the left foot forward, and stopping also with the montante in front of the face. You will undo the rule removing backward the left foot with a talho equal to the first, and the right foot with a revez, and take heed that the body must always turn toward where the montante cuts. At the end of the rule, while standing still, you will give a talho to the left shoulder and return the montante to again place the point on the ground as at the beginning, and all the rules having to do with the montante negro will have this ending.'),
('3003', 'Composed Rule 1', 'All the composed rules are counterpoint to the simple, and these the basis of the composed. And thus, planting the body with the left foot forward, you will give a talho from low to high, which will be accompanied forward by the right foot, stopping with the montante in font of the face. Then you will let fall the montante to the right to give an altibaxo putting in the left foot, and from where the montante comes to a stop you will give a talho from low to high accompanied by the right foot which you will move forward, stopping with the montante in front of the face. And then you will give a revez from low to high accompanied by the left foot which you will move forward, stopping with the montante in front of the face. Then you will let fall the montante to the left crossing the right arm over the left, and you will give an altibaxo revez moving the right foot forward, and from where the montante comes to a stop you will give a revez from low to high accompanied by the left foot which you will move forward, and stop with the montante in front of the face. Next, you will undo this rule with the same blows, actions, and steps, retreating backward until you place your body as it began the rule.'),
('3004', 'Simple Rule 2', 'You will place the body with the left foot forward, and putting in the right foot you will give a talho, such that the montante ends up with the point forward and the hands high in front of the eyes. Then first putting in the left foot you will give a revez, and pulling backward the same left foot you will give a talho, stopping again with the montante high, and removing the right foot you will give a revez, and a talho to the shoulder, and return the montante.'),
('3005', 'Composed Rule 2', 'You will raise the montante with the point forward in front of the right ear, with the left foot forward. Then you will move the right foot forward, and at the same time you will give a talho forward, bringing the montante high in front of the face in order to pass it over the head and behind the shoulders, such that it falls over the left arm to give a circling revez, and then a talho in a manner that you return the montante to its position high in front of the face. And from there, first moving the left foot forward, you will give a revez forward, ending with the montante high with the point forward, and passing it over head you will give a circling talho and a revez, returning the montante to the position in which you began the rule, which you will undo in the following manner: You will first remove the left foot and give a talho forward, ending with the montante point forward in front of the face, and then a talho,[3] and a circling revez,[3] and ending with the montante in the same place. Then you will remove the right foot backward with a revez forward that will stop with the montante high, and then a talho and a circling revez, replacing the montante how it started the rule.'),
('3006', 'Simple Rule 3', 'You will place the body with the left foot forward, and give a talho from behind while standing still, and another forward putting in the right foot; then a revez from behind while standing still, and another forward putting in the left foot. You will undo the rule with a talho from behind while standing still, and another forward removing the same left foot, and then a revez from behind while standing still, and another forward removing the right foot, to end how you began the rule.'),
('3007', 'Composed Rule 3', 'This rule serves to drive your adversaries before you. You will start by giving a talho from behind while standing still, and another forward putting in the right foot and making ready the thrust over the right arm, which you will give forward while standing still, then putting in the left foot with a revez, and one and another successive talhos to the right side, putting in the left foot, and then the right. Proceed in the rule readying the same thrust again, going forward as necessary until you finish with your adversaries.'),
('3008', 'Simple Rule 4', 'You will place the body with the left foot forward, and give a talho from behind while standing still and another forwards putting in the right foot, readying a thrust over the right arm, which you will give while standing still. Then you will put in the left foot with a revez, and removing backward this same left foot with a talho, you will ready another thrust that you will give while standing still, and removing backward the right foot you will give a revez to end in the state in which you began the rule.'),
('3009', 'Composed Rule 4', 'This rule is for fighting with people in front and behind; do it by giving a talho from behind with the opposite foot which is the left, and another forward putting in the right foot, readying a thrust over the right arm which you will give to the rear, removing again the right foot. Then the left foot will go with a revez, turning the body towards where you gave the thrust. Next you will give a talho forward with the same opposite foot, and another putting in the right foot readying the same thrust, which you will give with the right foot to the rear, and successively a revez entering with the left foot, and then the two talhos starting again with the opposite foot, all corresponding to the opposition against you.'),
('3010', 'Simple Rule 5', 'You will ready a thrust over the left arm planting the body with the right foot behind, and after removing it while standing still, moving the right foot forward you will give a talho, forming at the end of it another thrust over the right arm. Then you will put in the left foot with a revez, and while standing still you will give another thrust readying it over the left arm (for that is where all those that originate from revezes are formed). You will exit backward with the left foot with a talho, ending it with another thrust, that you will give while standing still (and all those that are formed from talhos are readied over the right arm), and exiting with the right foot you will give a revez, to end with your body as it was at the beginning of the rule.'),
('3011', 'Composed Rule 5', 'Placing the body almost profiled with the left foot forward, you will ready a thrust over the left arm, which you will give without taking a step. Then a talho from behind while standing still, and another forward putting in the right foot, readying a thrust over the right arm, which you will give while standing still. Then in the same manner a revez from behind and another forward putting in the left foot, readying the thrust to exit with a talho from behind and another forward removing the left foot, from which will originate another thrust. You will finish with a revez from behind, and another forward removing the right foot.'),
('3012', 'Simple Rule 6', 'This rule is called the Battle of the Montante, and is just one entry that you can use when encountering another montante. You will give a talho from behind while standing still, and another putting in the right foot to end in position, and with this stance you will always move towards the adversary, deflecting the opposing montante with a revez to the outside, assisted by a step that you will give forward, you will give a talho to the closest leg, recovering once again the stance. And thus advancing, you will again deflect to the same side to give a talho blow on the right arm of the adversary recovering again the stance of the feet, taking heed that all these revez deflections are done with the false edge.'),
('3013', 'Composed Rule 6', 'Although rarely does one montante meet with another, when it happens, you should value your knowledge about the nature of all movements, both the steps of the feet as well as the blows of the montante, all of which are derived from the movements of the sword. Based upon those of the sword, you can know the qualities of those for the montante, their weakness or their strength, with the single difference that all the deflections, parries and attacks of the montante must be helped by the movements of the body. In responding to the adversary, you must be prepared to act in accordance with the greater force required by the blows of the montante. This generalization will suffice for those who have the knowledge of the true skill of the sword, which is the foundation of all the arms that have been invented.'),
('3014', 'Simple Rule 7', 'This rule serves to deter people in a street and impede them moving from one end to another. Give a talho forwards in the direction where the people are, with a step forwards, in such a manner that you cross the road, and walking forth, when moving the same foot forward you will give another talho like the first. Turning to cross back over the road, you will face the same direction again, giving a revez with the right foot, and with the same foot following it with another. If the road is wide, in order to take it all, you will give more revezes or more talhos in the same manner.'),
('3015', 'Composed Rule 7', 'You will give a talho with the left foot from low to high, and a revez also towards the same end of the street, and from low to high putting in the right foot, and then another talho and revez in the same manner, and always you will stop the montante in front of the face. And if you want to turn about to the same place from where you started the rule, you will (after giving the last talho) give in the same direction a revez with the right foot, and then a talho with the left foot, and in each step you must give a blow, always from low to high, alternating talho and revez, until the people stop.'),
('3016', 'Simple Rule 8', 'This rule serves against shieldsmen. While standing still, you will give a talho from behind leaning the body, and another forward putting in the right foot and circling with the montante such that the face ends up turned towards where you gave the first talho, and then giving a revez while standing still and another putting in the left foot, circling around to the right side with the montante, and with the face towards where it was at the beginning. You will undo the rule exiting with a talho and another removing the left foot, circling again, then with a revez and another removing the right foot. Next you will give a talho from behind while standing still, and another putting in the right foot, and another removing backward the left foot and a revez while standing still, and another putting in the right foot and another removing the same right foot in return backward. These final blows are of the sixteenth composed rule, which after given, you can also insert into this rule the fourteenth simple rule.'),
('3017', 'Composed Rule 8', 'You will give a talho forward putting in the left foot, and another putting in the right foot, and circling, then a revez putting in the right foot, and another putting in the same right foot and circling also. Next a talho putting in the left foot, and another putting in the right foot and circling, then a revez putting in the right foot and another putting in the left foot and circling, then a talho putting in the left foot, and another putting in the right foot, and successively another putting in the same right foot to turn around when you want to return to the other direction, starting the rule again, with the steps wide and fast.'),
('3018', 'Simple Rule 9', 'This rule serves to fight in a narrow street. You will do it by giving a talho from low to high moving the right foot forward, and then letting the montante fall to the same right side you will give on that side an altibaxo, coming to situate a thrust with the pommel of the montante on the right shoulder, which you will give putting in the right foot, and you will commence the rule again, facing the other direction, with the same blows until it becomes necessary to turn about.'),
('3019', 'Composed Rule 9', 'Placing the body profiled with the left foot forward you will give a talho from low to high putting in the opposite foot which is the right, and next, with the opposite foot which will then be the left, a revez. Then with the right foot you will make a talho attack from low to high, coming to ready a thrust over the right arm, which you will give removing the right foot backward towards where you started the rule, and next you will ready a thrust such that the pommel is on the right shoulder, which you will give moving the right foot forward. And with the face turned you will start the rule again in the other direction if necessary, with the same postures, blows, steps, and thrusts that have been shown.'),
('3020', 'Simple Rule 10', 'This rule is called Guarding a Lady, presuming that she hides behind your shoulders, and you wish to defend her. You will place the body square with the compass of the feet a little wide, and you will give a talho moving the left foot one palm width forward, looking in the direction the montante goes, and stopping with in front of the face; and you will give a revez moving the right foot the same way, and a talho moving the left foot, and then a revez moving the right foot according to the same theory. Then you will give a talho while standing still and a revez removing the right foot, and a talho removing the left foot, and another revez removing the right foot. Here could come into play the two talhos and two revezes that are given while standing still and with the left arm held firmly against the body, which are commonly called Fly-Swatter and belong to the thirteenth composed rule.'),
('3021', 'Composed Rule 10', 'You will plant the body square and you will give a talho forward moving the left foot forward and angled to the left side, and from there you will turn to ready a thrust over the right arm that you will give with a step of the right foot towards the right side along the diagonal of the square. From there you will give a talho like the first moving the left foot along the left diagonal, and you will give a revez from low to high, moving the right foot along the right diagonal, and in a manner that from it you prepare a thrust over the left arm, which you will give it to the left side moving the left foot along the left diagonal, then you will turn to give a revez from low to high moving the right foot along the right diagonal. You will undo this rule removing the right foot with a talho readying a thrust over the right arm, that you will give while standing still, and while standing still another talho, and then a revez launching outwards the left foot to ready a thrust over the left arm which you will give while standing still, and then also while standing still a revez, and a talho launching outwards the right foot, and a revez removing the left foot.'),
('3022', 'Simple Rule 11', 'This rule is called Galley Gangway, and you do it giving forward a horizontal talho while standing still, and another putting in the right foot stopping with the montante in front of the face with the feet in the same position as at the start of the rule. Next you will give a horizontal revez while standing still, and another putting in the right foot. Then with the left foot forward, you will ready a thrust on the right shoulder, that you will have to give moving the right foot along the gangway, such that you end up facing the other direction, and you will start the rule in the opposite direction.'),
('3023', 'Composed Rule 11', 'You will place the right foot forward, and you will give a horizontal talho towards the left side, moving the left foot forward, and you will come to ready a thrust over the right arm, which you will give moving forward the right foot; then you will give another horizontal talho like the first moving the left foot forward and another thrust like the first moving the right foot forward with it. Then you will put in the left foot with a circling horizontal revez, which you will give while standing still, and from it you will give a talho moving the left foot, and starting the rule in the other direction, with the same movements already mentioned.'),
('3024', 'Simple Rule 12', 'This rule serves to fight with people in front and behind, and thus you will give a talho with the contrary foot, which is the left, readying a thrust over the right arm, which you will give while standing still. Then you will put in the left foot with a revez, and after circling through the right side with it, you will give a talho while standing still, readying in the same manner as the first another thrust, and after giving it while standing still, you will again put in the left foot with a revez, and you will follow the rule starting with the talho, moving always over the right foot when turning around.'),
('3025', 'Composed Rule 12', 'You will place the body square, and you will give a talho moving the left foot a little forward, accompanied by the right foot such that the stance is moderate, and you will ready a thrust over the right arm, which you will give moving the right foot along the line of infinity on the right side, accompanying it with the left. Then you will put in the left foot with a circling revez, and you will give a talho while standing still, and another removing the left foot backward, and another moving forward the right foot. Then you will ready a thrust over the right arm, which you will give moving forward the left foot, and a circling revez removing backward the right foot; and when circling always move forward the left foot, and if necessary you can start the rule again as described.'),
('3026', 'Simple Rule 13', 'This rule is called Guarding a Cloak, because it is used to defend one which has fallen to the ground, or which was deliberately dropped at your feet so as not to hinder you. You will plant the body in a wide stance, and give a talho while standing still, then another putting in the right foot, and another again putting in the same right foot, walking like a screw over the left. You will undo the rule by giving a revez while standing still, another putting in the left foot and another also putting in the same left foot. For the revezes you will move always over the right foot, which you will not remove from its place, likewise with the left when you give the talhos, which then serves as an axis for the body.'),
('3027', 'Composed Rule 13', 'This composed rule is done in the same manner as the simple, only after you give the first three talhos turning about the left foot, with the body stopped and firm, you must add a circling revez and a circling talho, and successively another revez and talho also circling like Fly-Swatter. And at the end of the three revezes that you give turning about the right foot, add again a circling talho and a circling revez, and successively another talho and another revez, with the left hand low and against the body to be more firm, and so the blows will be executed with more force.'),
('3028', 'Simple Rule 14', 'This rule serves against thrown weapons, or against hafted weapons for two hands. Planting the body firm with the montante in obtuse posture, the body a little inclined, and ready to give a talho on the weapon that is hurled at you or that is thrust at you, you will deflect it to the left side. Then giving a large jump while turning around, another talho that reaches the person who threw it; or else deflect with a revez, according to which side the opposing weapon is aimed, to give another revez with another jump with the body turning around and making a circle, in such a manner that you offend the adversary with a blow.'),
('3029', 'Composed Rule 14', 'This rule has two universal postures, the first is to place the body square with the right foot in front, the montante in obtuse angle along the right diagonal, such that the right hand rests in front of the belt to deflect the thrust aimed at the left breast with a talho, and immediately another with a jump, as described in the simple rule, and finish with a revez; or after you deflect with the first talho, ready the thrust over the right arm and give it moving the left foot forward and then the right, and finish putting in the left foot with a revez. The second posture is to place the right foot forward, and the montante in obtuse angle along the left diagonal to deflect by revez the thrusts aimed at the right side, and successively give another revez with the jump as described in the simple rule; or else after deflecting the thrust with the revez, to ready the thrust with the pommel on the right shoulder, which you will give moving the left foot forward followed by the right, and then finish putting in the left foot with the revez. In the first posture, if the adversary�s blow is aimed below the belt, you will deflect it with the montante moving in acute angle along the left diagonal, shifting the body with a moderate step along the line of infinity towards the right side, and letting fall the montante to the left side with the hands exchanged, you will put in successively the left foot, and following it the right foot with an altibaxo to ready the thrust over the right arm, and give it moving the left foot forward, to finish giving an altibaxo revez.'),
('3030', 'Simple Rule 15', 'This rule serves to separate people who are fighting. You will place the body almost square with the left foot a little forward, and you will give a talho to the left side from low to high, ending with the montante extended in front of the face, and moving the right foot forward; then you will give a revez from low to high, to the right side putting in the left foot, such that the montante again ends in front of the face. You will proceed giving a talho in the same manner as the first, and a revez in the same method as the first, and then a talho moving the right foot, such that the body ends up facing towards the left side, and successively another talho turning the body to where you started the rule putting in the right foot, and ending with the montante in front of the face on the left side. You will continue in the same way you began, if necessary.'),
('3031', 'Composed Rule 15', 'You will place the body with the left foot forward, and you will give a talho from low to high moving the right foot forward, bringing the montante to stop high in front of the head on the left side, in obtuse line along the diagonal, and from there you will give an altibaxo along the same left side circling the montante by the left shoulder; from there you will give a revez from low to high to the right side moving the left foot forward and ending with the montante high along the right diagonal in an obtuse line, then from there you will give an altibaxo circling the montante with the right arm. Then you will again give a talho like the first with your altibaxo in the same form, and a revez like the first with your altibaxo of the same method, and moving the feet in the same manner as the first steps; and wanting to turn about you will give a talho putting in the right foot, and another talho again putting in the right foot towards the side from which you began the rule, and begin it again with the face turned as at the beginning.'),
('3032', 'Simple Rule 16', 'This rule is for fighting in a wide road with people in front and behind. You will give a talho from behind while standing still and another forward putting in the right foot, readying the thrust over the right arm, which you will give forward while standing still. Then sensing people behind, you will ready from the thrust that you gave another with the pommel of the montante on the right shoulder, which you will give putting in the right foot toward the direction from where you started the rule. This gives rise to a revez which you will give putting in the left foot, and circling with the montante you will remove the right foot, and you will start the rule again.'),
('3033', 'Composed Rule 16', 'You will plant your left foot forward and you will give a talho raising the montante over the head to come to circle on the right side and you will give it while standing still; and then another talho putting in the right foot and another removing backward the left foot. Then you will ready a thrust over the right arm, which you will give while standing still, and another thrust with the pommel on the right shoulder which you will give moving the right foot forward. Then the left foot will go with the revez, which you will give removing backward the right foot, and another revez moving the left foot forward, from which will originate a thrust over the left arm which you will give while standing still, and from that a revez removing backward the right foot, ending in the first state in which you began the rule, and so you may proceed again, if necessary.');

INSERT INTO Translations.SectionTranslation
(
    SectionKey,
    TranslationKey,
    Translation,
    CreatedByUserKey,
    ModifiedByUserKey
)
SELECT s.SectionKey,
       @TranslationKey,
       s.Translation,
       -1,
       -1
FROM @Sections s;
