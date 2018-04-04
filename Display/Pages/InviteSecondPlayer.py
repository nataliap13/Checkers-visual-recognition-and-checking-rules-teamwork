from selenium.webdriver.common.by import By
from Locators import Locator

from selenium.webdriver.support.wait import WebDriverWait
from selenium.webdriver.support import expected_conditions as EC

class InviteSecondPlayer(object):

    def __init__(self, driver):
        self.driver = driver
        
        self.SecondPlayer = driver.find_element(By.XPATH, Locator.SecondPlayer)



    def click_InviteSecondPlayer(self,driver):
        self.SecondPlayer.click()
        textplace=WebDriverWait(driver, 20).until(
        EC.visibility_of_element_located((By.XPATH, Locator.AcceptGame)))


    
