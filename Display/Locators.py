class Locator(object):


#  Welcome 
        GuestButton="//*[@class='lbut lbprm']"
        KindOfRoom="//*[@class='selcsl']"
        ZalogujButton="//*[@class='lbut lbpbg']"

#  Login 
        #LoginButton="//*[contains(text(), 'zaloguj')]"
        Login="//*[@id='id']"
        Password="//*[@type='password']"
        EnterButton="//*[@type='submit']"

#  Start 
        StartButton="//*[@class='lbut lbprm']"

#  Choose Room
        ChooseKindOfRoom="//*[contains(text(), '#900')]"


#  NewGameTable 
        NewGame="//button[@class='butsys minwd']"

#  Players 
        Players="//*[contains(text(), 'obecni')]"
        TakeSide1="(//*[@class='butsys butsit'])[1]"
        TakeSide2="(//*[@class='butsys butsit'])[2]"

#  Invite 
        Invite="//button[contains(text(), 'zaproś')]"


#  InviteSecondPlayer  
        SecondPlayer="(//div[@class='ulnm' and contains(text(), 'piotrklient')])[2]"


#  AcceptGame
        AcceptGame="(//*[@class='butwb'])[2]"


 #  AcceptorDeclineInvite 
        Condition="//*[contains(text(), 'piotr1500 zaprasza do stołu')]"
        AcceptInvite= "(//button[@class='minw' and contains(text(), 'tak')])[1]"
        DeclineInvite=" (//button[@class='minw' and contains(text(), 'nie')])[1]"



