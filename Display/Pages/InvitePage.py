from selenium.webdriver.common.by import By
from Locators import Locator


class Invite(object):

    def __init__(self, driver):
        self.driver = driver
        
        self.Invite = driver.find_element(By.XPATH, Locator.Invite)


    def click_Invite(self):
        self.Invite.click()

    
