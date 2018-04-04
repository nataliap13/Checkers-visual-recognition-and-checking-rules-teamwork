import os, sys
parentddir = os.path.abspath(os.path.join(os.path.dirname(__file__), os.path.pardir))
sys.path.append(parentddir)

import unittest
import time
import random
import string
import sys
from selenium import webdriver
from selenium.webdriver.support.wait import WebDriverWait
from selenium.webdriver.support import expected_conditions as EC
from selenium.webdriver.common.by import By
from selenium.webdriver import ActionChains
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
from Pages.AcceptorDeclineInvitePage import AcceptorDeclineInvite



class Server(EnvironmentSetup):


    def test_Client(self):
        www="https://www.kurnik.pl/warcaby/"
# Using the driver instances created in EnvironmentSetup 
        driver = self.driver
        self.driver.get(www)
        self.driver.set_page_load_timeout(20)
        
    
        welcome=Welcome(driver)
        window_before = driver.window_handles[0]
        welcome.click_ZalogujButton()
# LOG IN
        login=Login(driver)
        login.setLogin("piotrklient")
        login.setPassword("TUWPISACHASLO")
        login.click_EnterButton()

        start=Start(driver)
        start.click_StartButton()
        window_after = driver.window_handles[1]
        driver.switch_to_window(window_after)


        choose900=Choose900(driver)
        choose900.click_ChoosekindofRoom()
        #tutaj wait czeka na  zaproszenie

        el=WebDriverWait(driver, 30).until(
        EC.visibility_of_element_located((By.XPATH, Locator.Condition)))

        AorDInvite=AcceptorDeclineInvite(driver)
        if(AorDInvite.check_Condition()==True):
            AorDInvite.click_AcceptInvite(driver)

            
        players=Players(driver)
        players.click_TakeSide2(driver) 
        #tutaj wait czeka na  accepttgame


        acceptgame=AcceptGame(driver)
        acceptgame.click_AcceptGame(driver)


        waiting = int(input("Waiting for you"))  

if __name__ == '__main__':
    unittest.main()
