from selenium.webdriver.common.by import By
from Locators import Locator

from selenium.webdriver.support.wait import WebDriverWait
from selenium.webdriver.support import expected_conditions as EC

class Players(object):

    def __init__(self, driver):
        self.driver = driver

        self.Players = driver.find_element(By.XPATH, Locator.Players)
        self.Invite=driver.find_element(By.XPATH, Locator.Invite)
        self.TakeSide1=driver.find_element(By.XPATH, Locator.TakeSide1)
        self.TakeSide2=driver.find_element(By.XPATH, Locator.TakeSide2)


    def click_Players(self):
        self.Players.click()

    def click_Invite(self):
        self.Invite.click()

    def click_TakeSide1(self):
        self.TakeSide1.click()

    def click_TakeSide2(self,driver):
        self.TakeSide2.click()
        textplace=WebDriverWait(driver, 10).until(
        EC.visibility_of_element_located((By.XPATH, Locator.AcceptGame)))


    
