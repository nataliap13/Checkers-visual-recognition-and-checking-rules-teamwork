from selenium.webdriver.common.by import By
from Locators import Locator

from selenium.webdriver.support.wait import WebDriverWait
from selenium.webdriver.support import expected_conditions as EC

class NewGameTable(object):

    def __init__(self, driver):
        self.driver = driver

        self.NewGame = driver.find_element(By.XPATH, Locator.NewGame)




    def click_NewGame(self,driver):
        self.NewGame.click()
        textplace=WebDriverWait(driver, 10).until(
        EC.visibility_of_element_located((By.XPATH, Locator.TakeSide1)))

    
