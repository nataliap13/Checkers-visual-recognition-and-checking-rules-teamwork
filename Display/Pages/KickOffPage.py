from selenium.webdriver.common.by import By
from Locators import Locator


class KickOff(object):

    def __init__(self, driver):
        self.driver = driver
        
        self.KickOff = driver.find_element(By.XPATH, Locator.KickOff)


    def click_Kickoff(self):
        self.KickOff.click()
    
