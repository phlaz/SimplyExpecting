using SimplyExpecting_V2.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace SimplyExpecting_V2.Data
{
	public static class DbContextExtensions
	{
		public static TContext NewContext<TContext>()
			where TContext : System.Data.Entity.DbContext, new()
		{
			using (var db = new TContext())
			{
				return db;
			}
		}
	}

	/// <summary>
	/// Extension Method for SimplExpectingDataContext to resolve to Db.Instance.
	/// </summary>
	public static class Db
	{
		/// <summary>
		/// Returns a new SimplyExpectingDataContext thats been wrapped in a using block to ensure disposal.
		/// </summary>
		/// <returns>SimplyExpectingDataContext</returns>
		public static SimplyExpectingDataContext Instance
		{
			get
			{
				return DbContextExtensions.NewContext<SimplyExpectingDataContext>();
			}
		}
	}

	public class SimplyExpectingDataContext : DbContext
	{
		public SimplyExpectingDataContext()
		{ }

		public DbSet<Content> PageContent { get; set; }

		public DbSet<ContentSection> WebPages { get; set; }

		public DbSet<MenuItem> MenuItems { get; set; }
	}

	public class DatabaseInitialier : DropCreateDatabaseIfModelChanges<SimplyExpectingDataContext>
	{
		protected override void Seed(SimplyExpectingDataContext context)
		{
			context.WebPages.AddRange(new[] {
				new ContentSection
				{
					Name = "Menu"
				},
				new ContentSection
				{
					Name = "Home"
				},
				new ContentSection
				{
					Name = "Pregnancy"
				},
				new ContentSection
				{
					Name = "PostPregnancy"
				},
				new ContentSection
				{
					Name = "HypnoBirthing"
				},
				new ContentSection
				{
					Name = "FAQ"
				},
				new ContentSection
				{
					Name = "ClasseAss"
				},

				new ContentSection
				{
					Name = "Contact"
				},
				new ContentSection
				{
					Name = "Testimonials"
				}
			});
			context.SaveChangesAsync();

			

			context.PageContent.AddRange(new[] {
				new Content
				{
					SectionId = SectionName.Menu,
					Version = 1
				},
				new Content
				{
					SectionId = SectionName.Home,
					Version = 1,
					Html =
					@"<div id=""quote"">
						<blockquote>
							<p>
								In giving birth to our babies, we may find that we give birth to new possibilities within ourselves.
							</p>
							<cite>
								Myla and Jon Kabat - Zinn,<br/>
								<a href = ""http://www.amazon.com/Everyday-Blessings-Inner-Mindful-Parenting/dp/B003BVK2RQ"" title = 
									""View this book on Amazon"" >
									Everyday Blessings: The Inner Work of Mindful Parenting
								</a>
							</cite>
						</blockquote>
					</div>
					<br />
					<p>
						Simply Expecting Pilates provides an expectant mother a way to connect with her body, mind and unborn child by teaching safe 
						and effective Pilates exercises. Pilates will keep her ever changing body strong and flexible throughout her pregnancy. 
						Pilates also allows her body to heal and recover much faster after the birth.
					</p>
					<p>
						Need to get back into shape after the birth? Simply Expecting has two post pregnancy classes a week. Bring your baby and 
						enjoy a Pilates class to get back into shape.
					</p>".Replace("\r", "").Replace("\n", "").Replace("\t", "")
				},
				new Content
				{
					SectionId = SectionName.Pregnancy,
					Version = 1,
					Html =
					@"<h1> Pregnancy Classes </h1>
					<p>
						If there was ever a time to start Pilates it is during your pregnancy. The classes are designed with the needs of 
						pregnant women in mind. Classes are kept small to ensure individual attention.
					</p>
					<p>
						Your body will go through many significant changes during pregnancy and each trimester brings its own changes and 
						challenges. These changes include weight gain, tiredness, feeling uncomfortable and being in some degree of pain 
						during your pregnancy. It's important to know that these are all normal!
				   </p>
					<p>
					   Whether this is your first, second or third pregnancy appropriate exercise is so important as it not only prepares 
					   your body for labour, birth and for a quicker recovery but also releases endorphins which are important for the well 
					   being of both mommy and baby.
					</p>"
				},
				new Content
				{
					SectionId = SectionName.PostPregnancy,
					Version = 1,
					Html =
					@"<h1> Post Pregnancy Classes </h1>
					<p>
						Postpartum rehabilitation exercises are very important for any new mother whether her baby is 3 months or 3 years. 
						The earlier you begin the better but there is no set time frame and it is never too late. These classes will help 
						strengthen the pelvic floor muscles, heal diastasis recti(separation of abdominals) and reactivate the abdominal 
						muscles.
					</p>
					<p>
						For normal vaginal delivery without any complications, six weeks is the estimated time that mothers can start with 
						the classes.If the birth did not go as planned or recovering from a C-Section, it is best to wait until 8 - 12 weeks 
						or with your medical practitioners consent.
					</p>

					<h2> Healing a Diastasis Recti </h2>
					<p>
						What is a Diastasis?
						Diastasis recti is a condition where the right and left sides of the rectus abdominus(the muscle that makes up the 
						front wall of the abdominals, also known as the “six - pack” muscle) spread apart at your midline.
					</p>
					<p>
						After pregnancy is the ideal time to begin working with a diastasis recti, trying to close it and avoiding making it 
						worse. Some of the exercises that we want to avoid initially are traditional Pilates abdominal exercises that include 
						flexion and flexion with rotation where the head, neck and chest lift off the floor. The reason we want to avoid these 
						exercises are because when a diastasis is present, especially in the early postpartum period, it compromises the ability 
						of the abdominal wall to function properly and to perform those exercises with proper muscular engagement, form and 
						control. Simply put until the muscles are repaired, able to fire correctly and support the movement and contraction 
						involved in certain exercises, it is advised that participation in such exercises be delayed (this goes for over weight 
						and obese non - postpartum individuals that have a diastasis as well).
					  </p>
					  <p>
						After birth, the muscles of the abdominal wall and the pelvic floor have been stretched, pushed, and challenged to their 
						maximum making it difficult for post partum women to properly feel, activate and use those muscles during a number of 
						different exercises, including those that involve traditional flexion.Also, because the muscles of the abdominal wall 
						have been stretched out toward the sides of the torso it can compromise the way in which the muscle fibers are firing. 
						This is why it is best to wait a couple months if not more until repair, healing and the initial re-strengthening has 
						occurred to introduce traditional flexion exercises again.
					</p>
					<p>
						<i>Group Classes and Private sessions are available.</i>
					</p>"
				},
				new Content
				{
					SectionId = SectionName.HypnoBirthing,
					Version = 1,
					Html =
					@"<div id=""hypnoBirthing"">
						<h1>HypnoBirth﻿ing® - The Mongan Method</h1>
						<p>
							HypnoBirthing® - The Mongan Method - is a unique method of relaxed, natural childbirth education, enhanced by self-hypnosis
							techniques.
						</p>
						<p>
							﻿HypnoBirthing is based on the belief that all babies should come into the world in an atmosphere of gentility and calm.
							It is a comprehensive ante natal course that teaches the parents-to-be that birth is natural, normal and healthy. Special
							attention is placed on breathing and deep relaxation techniques for a faster and more comfortable labour, often eliminating
							the need for chemical anesthesia, episiotomy and other medical interventions.
						</p>
						<p>
							This technique was developed by Marie Mongan in 1989. Back in the 1950’s when Marie discovered she was pregnant with her
							first child she got her hands on Dr Grantly Dick-Read’s book on Natural Childbirth and was very much looking forward to
							a natural relaxed birth.  Even though her labour was smooth and pain-free she was drugged for the birth and awoke sometime
							later, violently ill from the ether and was informed she had “delivered” a baby boy.
						</p>
						<p>
							The same happened at the birth of her second child and only after telling her doctor she would fire him if he didn’t allow
							her the birth she wanted, was she able to have the birth she wanted with her husband at her side which was unheard of in
							those days. Years later she helped her daughter birth in the same calm serene manner and as a result developed the
							HypnoBirthing®  program that is now recognized all over the world.
						</p>
						<p>
							The program uses Hypnosis as a tool. Hypnosis is easily described as ‘focused relaxation’ where the subconscious mind is
							open to suggestion and does not mean you will be ‘controlled’ by the person hypnotizing you.  Relaxation is proven to be
							one of the most effective tools for dealing with tension, stress and discomfort and together with visualization can help
							the body birth without the need for drugs. During the course, each couple with learn breathing, relaxation and
							visualization techniques that they will practice at home so they can call them up readily when they are needed during
							labour. This ensures that both mind and body are well prepared.
						</p>
						<p>
							The course is taught over 5 consecutive weeks of sessions of 2.5 hours each. All course materials, HypnoBirthing®
							textbook and relaxation cd are provided. Any women over 20 weeks and her birthing companion (husband, partner, mother,
							doula etc) may attend.
						</p>
						<p>
							<address>
								<em>Please email to enquire about the next available course -</em>
								<a itemscope itemprop=""email"" href=""mailto:simplyexpecting @gmail.com"" title=""simplyexpecting@gmail.com"">simplyexpecting@gmail.com</a>
							</address>
						</p>
						<aside>
							<p>
								<q> We have convinced ourselves that labor is risky</q>, says Marie Mongan, MEd, MHy, founder of HypnoBirthing® -The 
								Mongan Method. Fear during labor activates our primal fight - or - flight mechanism, causing stress hormones called 
								catecholamines to slow down digestion, make the heart speed up, force blood to the arms and legs, and ultimately 
								deplete blood flow to the uterus, creating uterine pain and hindering the labor process.
							</p>
							<p>
								According to Mongan, who is a hypnotherapist and hypnoanesthesiologist, it is physically impossible for the body to be 
								relaxed and in fight - or - flight mode.By replacing fear with relaxation, a different set of chemicals come into play: 
								oxytocin, labor hormones called prostaglandins, and endorphins combine to relax the muscles and create a sense of comfort.
							</p>
						</aside>
					</div>"
				},
				new Content
				{
					SectionId = SectionName.FAQ,
					Version = 1,
					Html =
					@"<div id=""faq"">
						<hgroup>
							<h1>Frequently Asked Questions</h1>
						
						</hgroup>
						<ol>
							<li>
								<h2>Need to know a little more about Pre and Postnatal Pilates?</h2>
							</li>
							<li>
								<article>
									<h3>Is it safe to start Pilates while I am pregnant?</h3>
									Absolutely. Pilates is perfectly safe to do during pregnancy. It is a great idea to keep your body in top shape during
									the pregnancy and to help recovery after the baby is born. The sooner you start the better. However if you have had a
									history of miscarriages or prior complications during a pregnancy it is best to consult with your midwife/doctor before
									you start.
								</article>
							</li>
							<li>
								<article>
									<h3>Is Yoga or Pilates best during pregnancy?﻿﻿</h3>
									Both Pilates and Yoga help prepare the mind and body for pregnancy and birth using breathing techniques and specific
									exercises to open the hips and strengthen the core. Neither one is better than the other and it comes down to personal
									preference. Yoga and Pilates are very complimentary to each other and if you can why not do both.
								</article>
							</li>
							<li>
								<article>
									<h3>How much are the classes?</h3>
									Classes are R100 per class. Fees are paid in advance for 4 weeks. It is up to you how many classes you are able to
									attend in the week.
								</article>
							</li>
							<li>
								<article>
									<h3>Will Pilates help me to n​ot pick up weight?</h3>
									​It is normal and healthy to pick up weight during your pregnancy. Your midwife/doctor will be able to work out how
									much weight you should pick up based on your usual weight. Pregnancy is a time to embrace the changes that happen
									to your body.
								</article>
							</li>
							<li>
								<article>
									<h3>When can I start Pilates after I have had the baby?</h3>
									It is recommended you start Pilates 6 weeks after the birth of the baby. If the baby was born via Caesarian Section
									please wait until the incision has healed and your have gotten clearance from your doctor.
								</article>
							</li>
						</ol>
					</div>"
				},
				new Content
				{
					SectionId = SectionName.Classes,
					Version = 1,
					Html =
					@"<h1>Timetable and Services</h1>
					<p>
						<span itemprop=""price"">Private Pilates Session - R270</span><br />
						<span itemprop=""price"">Group Classes - R120</span>
					</p>﻿
					<p>
						<time itemprop=""openingHours"" datetime=""Mo 10:00-11:00"">Monday 10-11am - Postnatal Pilates Class (Thrupps Centre)</time><br />
						<time itemprop=""openingHours"" datetime=""Tu 18:30-19:30"">Tuesday 6:30-7:30pm - Pregnancy Class (Thrupps Centre)</time><br />
						<time itemprop=""openingHours"" datetime=""We 17:30-18:30"">Wednesday 5:30-6:30pm - Pregnancy Class (Ladybird Cnr Linksfield Clinic)</time><br />
						<time itemprop=""openingHours"" datetime=""Sa 09:00-10:00"">Saturday 9-10am - Pregnancy Class (Thrupps Centre)</time>
					</p>﻿
					<p>
						﻿<span itemprop=""price"">HypnoBirthing Antenatal Course (including all materials)</span><br />
						<span itemprop=""price"">Group - R1850</span><br />
						<span itemprop=""price"">Private (at venue) - R2500</span><br />
						<span itemprop=""price"">Private (at home) - R3000</span>
					</p>﻿﻿
					<p>
						<span itemprop=""price"">Breech Turning session - R300</span><br />
						<span itemprop=""price"">Movement for Labour Workshop - R300</span>
					</p>"
				},
				new Content
				{
					SectionId = SectionName.Contact,
					Version = 1,
					Html =
					@"<h1>Simply Expecting Pilates</h1>
					Need more info? Leave a message in the block below and ask your question
					<form id=""contactForm"" action=""./api/contactform"" enctype=""multipart/form-data"" method=""post"">
						<input title=""Your name"" placeholder=""name"" type=""text"" autofocus />
						<input title=""Your email address"" placeholder=""email"" type=""email"" />
						<input title=""The subject of your message"" placeholder=""subject"" type=""text"" />
						<textarea title=""Your message"" placeholder=""type your message..."" cols=""50"" rows=""10""></textarea>
						<input type=""submit"">submit</input>
					</form>"
				},
				new Content
				{
					SectionId = SectionName.Testimonials,
					Version = 1,
					Html =
					@"<h2>Client Testimonials</h2>
					<article>
						The pilates classes were fantastic as they helped to keep me in shape during pregnancy. Classes were very relaxing and
						also helped to ease back and leg aches and pains.
						﻿Kayla
					</article>
					﻿<article>
						﻿I really enjoyed the pregnancy Pilates especially because it was something new for me to do during pregnancy that helped
						me stay in 'shape'. The breathing techniques were a saving grace during my labour. As this was my third pregnancy it was
						extremely taxing on my body. I really needed all the breathing techniques they helped me stay focused. Thank you Michelle
						you and your class were a big help during my pregnancy and labour.
						​﻿Cherlaine
					</article>
					﻿<article>
						<p>
							﻿My husband and I attended the HypnoBirthing Course which we thoroughly enjoyed. I made use of the breathing and
							relaxation techniques during my labour. Without the techniques I would not have been able to cope with my 48 hour
							labour which culminated in me giving natural birth in a hospital!
						</p>
						<p>
							Michelle was patient and went out of her way to help us understand more advanced topics with regards to pregnancy
							and child birth. Michelle’s passion and concern even continued beyond the classes as she followed up with our queries
							and showed genuine interest with our baby’s progress and well-being.
						</p>
						<p>
							We are truly grateful to her for helping us and would highly recommend the HypnoBirthing Course to anyone who is
							looking forward to a calm and less painful birth.
							New Parents - Monica and Neill
						</p>
					</article>"
				}
			});

			context.SaveChanges();


			context.MenuItems.Add(
				new MenuItem
				{
					ContentId = SectionName.Home,
					Caption = "home",
					Title = "Simply Expecting Pilates."
				});
			context.SaveChanges();

            var birth = new MenuItem
			{
				Caption = "birth"
			};

			context.MenuItems.AddRange(new[]
				{
					birth,
					new MenuItem
					{
						ContentId = SectionName.Pregnancy,
						Caption = "pregnancy",
						Title = "Pilates during pregnancy",
                        Parent = birth
					},
					new MenuItem
					{
						ContentId = SectionName.PostPregnancy,
						Caption = "post pregnancy",
						Title= "Pilates after pregnancy",
                        Parent = birth
					},
					new MenuItem
					{
						ContentId = SectionName.HypnoBirthing,
						Caption = "hypnobirthing&reg;",
						Title = "HypnoBirthing Antinatal classes",
                        Parent = birth
					},
					new MenuItem
					{
						ContentId = SectionName.FAQ,
						Caption = "frequently asked questions",
						Title="Frequently Asked Questions",
                        Parent = birth
					}
					,new MenuItem
					{
						ContentId = SectionName.Classes,
						Caption = "pilates",
                        Title="Class Times"
					}
					,new MenuItem
					{
						ContentId = SectionName.Contact,
						Caption = "contact",
						Title="Find out where we are or send me a question"
					}
			});
			context.SaveChanges();
		}
	}
}