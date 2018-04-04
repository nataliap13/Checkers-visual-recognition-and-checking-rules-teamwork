from selenium.webdriver.common.by import By
from Locators import Locator

from selenium.webdriver.support.wait import WebDriverWait
from selenium.webdriver.support import expected_conditions as EC
from selenium.webdriver.common.by import By

class AcceptGame(object):

    def __init__(self, driver):
        self.driver = driver

        self.AcceptGame = driver.find_element(By.XPATH, Locator.AcceptGame)




    def click_AcceptGame(self,driver):
        self.AcceptGame.click()
        el=WebDriverWait(driver, 300).until(
        EC.visibility_of_element_located((By.XPATH, "//*[contains(@style,'top: 504') and contains(@style,'left: 84')]")))


