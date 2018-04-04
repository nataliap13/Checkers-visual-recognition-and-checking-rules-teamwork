from selenium.webdriver.common.by import By
from Locators import Locator


class Start(object):

    def __init__(self, driver):
        self.driver = driver

        self.StartButton = driver.find_element(By.XPATH, Locator.StartButton)




    def click_StartButton(self):
        self.StartButton.click()

    
