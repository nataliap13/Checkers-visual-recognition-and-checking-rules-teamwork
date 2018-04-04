from selenium.webdriver.common.by import By
from Locators import Locator

from selenium.webdriver.support.wait import WebDriverWait
from selenium.webdriver.support import expected_conditions as EC
from selenium.webdriver.common.by import By

class Choose900(object):

    def __init__(self, driver):
        self.driver = driver

        self.ChooseKindOfRoom = driver.find_element(By.XPATH, Locator.ChooseKindOfRoom)


    def click_ChoosekindofRoom(self,):
        self.ChooseKindOfRoom.click()
        
    
