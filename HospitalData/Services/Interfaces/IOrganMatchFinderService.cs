﻿using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalData.Services
{
	public interface IOrganMatchFinderService
	{
		List<DonatedOrgan> GetHasOrganList(int waitingId);
	}
}
