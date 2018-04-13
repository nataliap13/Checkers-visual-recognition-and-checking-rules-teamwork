import os, sys
parentddir = os.path.abspath(os.path.join(os.path.dirname(__file__), os.path.pardir))
sys.path.append(parentddir)

import unittest
import time
import random
import string
from selenium import webdriver
from Locators import Locator
from EnvironmentSetUp import EnvironmentSetup
from Pages.RoomPage import Room
from Pages.WelcomePage import Welcome
from Pages.Choose900Page import Choose900
from Pages.NewGameTablePage import NewGameTable
from Pages.LoginPage import Login
from Pages.StartPage import Start
from Pages.PlayersPage import Players
from Pages.InvitePage import Invite
from Pages.InviteSecondPlayer import InviteSecondPlayer
from Pages.AcceptGamePage import AcceptGame
from Tests.Test_Movement import Movement
class Server(EnvironmentSetup):


    def test_FirstPlayer(self):
        www="https://www.kurnik.pl/warcaby/"
# Using the driver instances created in EnvironmentSetup 
        driver = self.driver
        self.driver.get(www)
        self.driver.set_page_load_timeout(20)

# Creating object class Main

        
        welcome=Welcome(driver)
        window_before = driver.window_handles[0]
        welcome.click_ZalogujButton()
       

        login=Login(driver)
        login.setLogin("piotr1500")
        login.setPassword("Haslo12!")
        login.click_EnterButton()


        start=Start(driver)
        start.click_StartButton()
        window_after = driver.window_handles[1]
        driver.switch_to_window(window_after)
        

        print("Waiting  choose900")


        choose900=Choose900(driver)
        choose900.click_ChoosekindofRoom()


        newgame=NewGameTable(driver)
        newgame.click_NewGame(driver)  
        #tutaj wait

      
        players=Players(driver)
        players.click_TakeSide1()
        players.click_Players()
        
      
        #Just for show the list who want  invite
        invite=Invite(driver)
        invite.click_Invite()

        
        #Invite Client (piotrklient)
        invitesecondplayer=InviteSecondPlayer(driver)
        invitesecondplayer.click_InviteSecondPlayer(driver)
        #tutaj wait

        acceptgame=AcceptGame(driver)
        acceptgame.click_AcceptGame(driver)


        #time.sleep(1000)
        Coordinates=driver.find_element_by_xpath("//*[contains(@style,'top: 504') and contains(@style,'left: 84')]")
        while(True):
            print("Before click")              
            #pierwsza dodatnia to w prawo, druga ujemna to w gore
            a1,a2=Movement.Get_and_Convert()
            action = webdriver.common.action_chains.ActionChains(driver)
            action.move_to_element_with_offset(Coordinates,a1, a2)
            action.click()
            action.perform()
            print("After click")
        
        

if __name__ == '__main__':
    unittest.main()
