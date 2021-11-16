INSERT INTO [Weights] 
			(TermId, Input, [Count]) 
VALUES 
((select Id from Terms where [Name] = 'microphone'), 'mic', 10),
((select Id from Terms where [Name] = 'microscope'), 'mic', 5),
((select Id from Terms where [Name] = 'microscope'), 'micr', 11),
((select Id from Terms where [Name] = 'microsoft'), 'micr', 1)
